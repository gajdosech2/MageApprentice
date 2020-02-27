using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMovement : MonoBehaviour
{
    const float TILE_SIZE = 1.666f;

    const float WAIT_TIME = 1.0f;
    bool on_chessboard = false;
    float time_entered = 0.0f;

    const float MOVE_DISTANCE = 2.35f;
    float move_speed = 2.0f;
    float move_lerp = 1.0f;
    Vector3 start_position = Vector3.zero;

    const int ROTATION_AMOUNT = 90;
    int rotation_dir = 1;
    float rotation_speed = 0.9f;
    float rotation_lerp = 1.0f;
    float start_rotation = -45;

    CharacterController controller;
    PlayerMovement normal_movement;
    PlayerController player;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        normal_movement = GetComponent<PlayerMovement>();
        player = GetComponent<PlayerController>();
    }

    public void Tile()
    {
        int col = (int)(transform.position.x / TILE_SIZE);
        int row = (int)(transform.position.z / TILE_SIZE);
        Debug.Log("Col: " + col + ", Row: " + row);
    }

    void Move()
    {
        player.move_direction = Vector3.zero;

        if (move_lerp >= 1.0f && rotation_lerp >= 1.0f)
        {
            Tile();
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
            {
                rotation_dir = (Input.GetAxisRaw("Horizontal") > 0) ? 1 : -1;
                rotation_lerp = 0.0f;
                start_rotation = transform.rotation.eulerAngles.y;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                move_lerp = 0.0f;
                start_position = transform.position;
            }
        }

        else if (move_lerp < 1.0f)
        {
            player.move_direction.x = 1.0f;
            move_lerp += move_speed * Time.deltaTime;
            transform.position = Vector3.Lerp(start_position, start_position + controller.transform.forward * MOVE_DISTANCE, move_lerp);
        }

        else if (rotation_lerp < 1.0f)
        {
            rotation_lerp += rotation_speed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, start_rotation, 0), Quaternion.Euler(0, start_rotation + rotation_dir * ROTATION_AMOUNT, 0), rotation_lerp);
        }
    }

    void Update()
    {
        if (on_chessboard && Time.time - time_entered > WAIT_TIME)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Leave();
            }
            Move();
        }
    }

    void Leave()
    {
        on_chessboard = false;
        controller.enabled = true;
        normal_movement.enabled = true;
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name.Equals("ChessBoard") && time_entered == 0.0f)
        {
            controller.enabled = false;
            transform.position = new Vector3(7.5f, 0.0f, 0.8333f);
            transform.rotation = Quaternion.Euler(0, 45, 0);
            on_chessboard = true;
            time_entered = Time.time;
            player.move_direction = Vector3.zero;
            normal_movement.enabled = false;
        }
	}
}
