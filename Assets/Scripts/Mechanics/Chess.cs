using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour
{
    public static Chess instance;
    public const float TILE_SIZE = 1.666f;

    public GameObject gui;
    public GameObject enemy_text;
    public GameObject player_text;
    public GameObject info;
    public Transform player;
    public CameraController camera;

    [HideInInspector]
    public bool enemy_turn = false;
    public bool end = false;

    public List<GameObject> enemies;
    int active_enemy;

    const float MOVE_DISTANCE = 2.35f;
    float move_lerp = 0.0f;
    Vector3 start_position = Vector3.zero;

    const int ROTATION_AMOUNT = 90;
    float rotation_lerp = 1.0f;
    float start_rotation = -45;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Enemy()
    {
        enemy_turn = true;
        active_enemy = 0;
        camera.target = enemies[active_enemy].transform;

        move_lerp = 0.0f;
        start_position = enemies[active_enemy].transform.position;
        rotation_lerp = 0.0f;
        start_rotation = enemies[active_enemy].transform.rotation.eulerAngles.y;
        Switch();
    }

    public bool Check(int row, int col)
    {
        foreach(GameObject enemy in enemies)
        {
            int enemy_row = (int)(enemy.transform.position.z / TILE_SIZE);
            int enemy_col = (int)(enemy.transform.position.x / TILE_SIZE);
            if (row == enemy_row && col == enemy_col)
            {
                return false;
            }
        }
        return true;
    }

    public void Switch()
    {
        player_text.SetActive(!enemy_turn);
        enemy_text.SetActive(enemy_turn);
    }

    bool Bounds()
    {
        Vector3 new_position = enemies[active_enemy].transform.position + enemies[active_enemy].transform.forward * MOVE_DISTANCE;

        if (new_position.x < 0 || new_position.z < 0 || new_position.x > 15 || new_position.z > 15)
        {
            return false;
        }

        int row = (int)(new_position.z / TILE_SIZE);
        int col = (int)(new_position.x / TILE_SIZE);

        int other_row = (int)(player.position.z / TILE_SIZE);
        int other_col = (int)(player.position.x / TILE_SIZE);

        if (row == other_row && col == other_col)
        {
            return false;
        }

        foreach (GameObject enemy in enemies)
        {
            if (enemy != enemies[active_enemy])
            {
                other_row = (int)(enemy.transform.position.z / TILE_SIZE);
                other_col = (int)(enemy.transform.position.x / TILE_SIZE);
                if (row == other_row && col == other_col)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void Turn()
    {
        if (rotation_lerp < 1.0f)
        {
            rotation_lerp += 0.9f * Time.deltaTime;
            enemies[active_enemy].transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, start_rotation, 0), Quaternion.Euler(0, start_rotation + ROTATION_AMOUNT, 0), rotation_lerp);
        }
        else if (move_lerp == 0.0f && !Bounds())
        {
            rotation_lerp = 0.0f;
            start_rotation = enemies[active_enemy].transform.rotation.eulerAngles.y;
        }
        else if (move_lerp < 1.0f)
        {
            move_lerp += 2.0f * Time.deltaTime;
            enemies[active_enemy].transform.position = Vector3.Lerp(start_position, start_position + enemies[active_enemy].transform.forward * MOVE_DISTANCE, move_lerp);
        }
        else
        {
            enemy_turn = false;
            camera.target = player;
            Switch();
        }
    }

    void End()
    {

    }

    void Update()
    {
        End();
        if (Input.GetButton("Submit"))
        {
            info.SetActive(false);
        }
        if (enemy_turn)
        {
            Turn();
        }
    }

}
