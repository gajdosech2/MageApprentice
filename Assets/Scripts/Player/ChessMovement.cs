using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMovement : MonoBehaviour
{
    public const float TILE_SIZE = 1.666f;
    Vector3 START_POS = new Vector3(7.5f, 0.0f, 7.5f);

    bool on_chessboard = false;
    bool start = false;

    const float MOVE_DISTANCE = 2.35f;
    float move_speed = 2.0f;
    float move_lerp = 0.0f;
    Vector3 start_position = Vector3.zero;

    const int ROTATION_AMOUNT = 90;
    int rotation_dir = 1;
    float rotation_speed = 0.9f;
    float rotation_lerp = 1.0f;
    float start_rotation = -45;

    PlayerMovement normal_movement;
    PlayerController player;

    void Start()
    {
        normal_movement = GetComponent<PlayerMovement>();
        player = GetComponent<PlayerController>();
    }

    bool Bounds()
    {
        Vector3 new_position = transform.position + transform.forward * MOVE_DISTANCE;
        if (new_position.x < 0 || new_position.z < 0 || new_position.x > 15 || new_position.z > 15)
        {
            return false;
        }
        return Chess.instance.Free((int)(new_position.z / TILE_SIZE), (int)(new_position.x / TILE_SIZE));
    }

    void Move()
    {
        player.move_direction = Vector3.zero;

        if (move_lerp < 1.0f)
        {
            player.move_direction.x = 1.0f;
            move_lerp += move_speed * Time.deltaTime;
            transform.position = Vector3.Lerp(start_position, start_position + transform.forward * MOVE_DISTANCE, move_lerp);
            if (move_lerp >= 1.0f)
            {
                Chess.instance.Enemy();
            }
        }

        else if (rotation_lerp < 1.0f)
        {
            rotation_lerp += rotation_speed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, start_rotation, 0), Quaternion.Euler(0, start_rotation + rotation_dir * ROTATION_AMOUNT, 0), rotation_lerp);
        }

        else if (!Chess.instance.enemy_turn)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
            {
                rotation_dir = (Input.GetAxisRaw("Horizontal") > 0) ? 1 : -1;
                rotation_lerp = 0.0f;
                start_rotation = transform.rotation.eulerAngles.y;
            }
            if (Input.GetAxisRaw("Vertical") > 0 && Bounds())
            {
                move_lerp = 0.0f;
                start_position = transform.position;
            }
        }
    }

    void Update()
    {
        if (on_chessboard)
        {
            if (Chess.instance.end)
            {
                Leave();
            }
            else if (start == false)
            {
                move_lerp += move_speed/4 * Time.deltaTime;
                transform.position = Vector3.Lerp(start_position, START_POS, move_lerp);
                if (move_lerp >= 1.0f)
                {
                    start = true;
                    Chess.instance.gui.SetActive(true);
                    Chess.instance.Switch();
                }
            }
            else 
            {
                Move();
            }
        }
    }

    void Leave()
    {
        on_chessboard = false;
        normal_movement.enabled = true;
        Chess.instance.gui.SetActive(false);
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name.Equals("ChessBoard") && start == false)
        {
            on_chessboard = true;
            normal_movement.enabled = false;
            transform.rotation = Quaternion.Euler(0, 45, 0);
            player.move_direction = Vector3.zero;
            start_position = transform.position;
        }
	}
}
