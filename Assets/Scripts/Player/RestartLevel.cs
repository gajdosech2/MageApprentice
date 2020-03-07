using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    public string level = "Level00";
    PlayerMovement player_movement;
    PlayerController player_controller;
    PlayerCollector player_collector;

    void Start()
    {
        GameObject mage = GameObject.Find("MageCharacter");
        player_movement = mage.GetComponent<PlayerMovement>();
        player_controller = mage.GetComponent<PlayerController>();
        player_collector = mage.GetComponent<PlayerCollector>();
    }

    void Update()
    {
        player_movement.enabled = false;
        player_controller.move_direction = Vector3.zero;
        player_controller.enabled = false;
        if (Input.GetButton("Submit"))
        {
            ManagerGame.total_score = Mathf.Max(ManagerGame.total_score - player_collector.level_score, 0);
            Application.LoadLevel(level);
        }
    }
}
