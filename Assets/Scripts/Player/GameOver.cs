using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public JoyButton enter;
    public string level = "Level00";
    PlayerCollector player_collector;

    void Start()
    {
        PlayerInterface player = GameObject.Find("Player").GetComponent<PlayerInterface>();
        player_collector = player.character.GetComponent<PlayerCollector>();
        PlayerController controller = player.character.GetComponent<PlayerController>();
        controller.move_direction = Vector3.zero;
        controller.enabled = false;
        player.character.GetComponent<Animator>().SetInteger("State", 3);
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") || enter.GetDown())
        {
            ManagerGame.total_score = Mathf.Max(ManagerGame.total_score - player_collector.level_score, 0);
            Application.LoadLevel(level);
        }
    }
}
