using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chess : MonoBehaviour
{
    public static Chess instance;
    public const float TILE_SIZE = 1.666f;

    public JoyButton enter;
    public GameObject gui;
    public GameObject enemy_text;
    public GameObject player_text;
    public GameObject info;
    public GameObject over;
    public GameObject pickups;

    public Transform player;
    public UnityEvent OnActivate;
    public List<Light> lights;

    public List<GameObject> enemies;
    int active_enemy;
    int target_row;
    int target_col;

    [HideInInspector]
    public bool enemy_turn = false;
    [HideInInspector]
    public bool end = false;

    float start_rotation = -45;
    float rotation_lerp = 1.0f;
    float move_lerp = 0.0f;
    Vector3 start_position = Vector3.zero;
    Camera active_camera;

    void Start()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        active_camera = player.GetComponentInChildren<Camera>();
    }

    public bool Free(int row, int col)
    {
        if (Check(row, col))
        {
            return false;
        }
        foreach (GameObject enemy in enemies)
        {
            int enemy_row = (int)(enemy.transform.position.z / TILE_SIZE);
            int enemy_col = (int)(enemy.transform.position.x / TILE_SIZE);
            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    int other_row = enemy_row + i;
                    int other_col = enemy_col + j;
                    if (row == other_row && col == other_col)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void Switch()
    {
        player_text.SetActive(!enemy_turn);
        enemy_text.SetActive(enemy_turn);

        active_camera.enabled = false;
        if (enemy_turn)
        {
            active_camera = enemies[active_enemy].GetComponentInChildren<Camera>();
        }
        else
        {
            active_camera = player.GetComponentInChildren<Camera>();
        }
        active_camera.enabled = true;
    }

    public void Enemy()
    {
        Win();
        if (!end)
        {
            enemy_turn = true;
            Pick();
            Choose();
            Switch();
            move_lerp = 0.0f;
            start_position = enemies[active_enemy].transform.position;
        }
    }

    bool Check(int row, int col, int index = -1)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (i != index)
            {
                int other_row = (int)(enemies[i].transform.position.z / TILE_SIZE);
                int other_col = (int)(enemies[i].transform.position.x / TILE_SIZE);
                if (row == other_row && col == other_col)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Pick()
    {
        active_enemy = Random.Range(0, enemies.Count - 1);
        int player_row = (int)(player.position.z / TILE_SIZE);
        int player_col = (int)(player.position.x / TILE_SIZE);
        int row = (int)(enemies[active_enemy].transform.position.z / TILE_SIZE);
        int col = (int)(enemies[active_enemy].transform.position.x / TILE_SIZE);
        int diff_row = Mathf.Abs(row - player_row);
        int diff_col = Mathf.Abs(col - player_col);
        for (int i = 0; i < enemies.Count; i++)
        {
            row = (int)(enemies[i].transform.position.z / TILE_SIZE);
            col = (int)(enemies[i].transform.position.x / TILE_SIZE);
            int new_diff_row = Mathf.Abs(row - player_row);
            int new_diff_col = Mathf.Abs(col - player_col);

            if ((diff_row == 1 && diff_col == 1) ||
                    ((new_diff_row != 1 || new_diff_col != 1) &&
                     (new_diff_row + new_diff_col < diff_row + diff_col)))
            {
                active_enemy = i;
            }
        }
    }

    void Choose()
    {
        int current_row = (int)(enemies[active_enemy].transform.position.z / TILE_SIZE);
        int current_col = (int)(enemies[active_enemy].transform.position.x / TILE_SIZE);

        int min_diff = 999;
        target_row = -1;
        target_col = -1;
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                int new_row = current_row + i;
                int new_col = current_col + j;
                if (new_row < 0 || new_row > 8 || new_col < 0 || new_col > 8)
                {
                    continue;
                }

                int player_row = (int)(player.position.z / TILE_SIZE);
                int player_col = (int)(player.position.x / TILE_SIZE);
                if (new_row == player_row && new_col == player_col)
                {
                    continue;
                }

                if (Check(new_row, new_col))
                {
                    continue;
                }

                int diff = Mathf.Abs(new_row - player_row) + Mathf.Abs(new_col - player_col);
                if (diff < min_diff)
                {
                    min_diff = diff;
                    target_row = new_row;
                    target_col = new_col;
                }
            }
        }
    }

    void Turn()
    {
        if (rotation_lerp < 1.0f)
        {
            rotation_lerp += 0.5f * Time.deltaTime;
            enemies[active_enemy].transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, start_rotation, 0), Quaternion.Euler(0, start_rotation + 90, 0), rotation_lerp);
        }
        else if (move_lerp == 0.0f && !Target())
        {
            rotation_lerp = 0.0f;
            start_rotation = enemies[active_enemy].transform.rotation.eulerAngles.y;
        }
        else if (move_lerp < 1.0f)
        {
            move_lerp += 1.0f * Time.deltaTime;
            enemies[active_enemy].transform.position = Vector3.Lerp(start_position, start_position + enemies[active_enemy].transform.forward * 2.35f, move_lerp);
        }
        else
        {
            enemy_turn = false;
            Switch();
            Lose();
        }
    }

    bool Target()
    {
        Vector3 new_position = enemies[active_enemy].transform.position + enemies[active_enemy].transform.forward * 2.35f;
        return (int)(new_position.z / TILE_SIZE) == target_row && (int)(new_position.x / TILE_SIZE) == target_col;
    }

    void Update()
    {
        if (Input.GetButton("Submit") || enter.GetDown())
        {
            foreach (Light light in lights)
            {
                light.intensity = 4;
            }
            info.SetActive(false);
        }
        if (enemy_turn)
        {
            Turn();
        }
    }

    void Lose()
    {
        bool can_move = false;
        int player_row = (int)(player.position.z / TILE_SIZE);
        int player_col = (int)(player.position.x / TILE_SIZE);
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                int new_row = player_row + i;
                int new_col = player_col + j;

                if (new_row < 0 || new_row > 8 || new_col < 0 || new_col > 8)
                {
                    continue;
                }
                if (!Free(new_row, new_col))
                {
                    continue;
                }
                can_move = true;
            }
        }
        if (!can_move)
        {
            end = true;
            pickups.SetActive(false);
            over.SetActive(true);
        }
    }

    void Win()
    {
        int player_row = (int)(player.position.z / TILE_SIZE);
        int player_col = (int)(player.position.x / TILE_SIZE);
        if ((player_row == 0 && player_col == 0) ||
                (player_row == 8 && player_col == 8) ||
                (player_row == 0 && player_col == 8) ||
                (player_row == 8 && player_col == 0))
        {
            end = true;
            pickups.SetActive(false);
            foreach(Light light in lights)
            {
                light.intensity = 0;
            }
            OnActivate.Invoke();
            OnActivate.RemoveAllListeners();
        }
    }
}
