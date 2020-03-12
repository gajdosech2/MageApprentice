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
        GameObject mage = GameObject.Find("MageCharacter");
        player_collector = mage.GetComponent<PlayerCollector>();
        mage.GetComponent<PlayerMovement>().enabled = false;
        mage.GetComponent<PlayerController>().move_direction = Vector3.zero;
        mage.GetComponent<PlayerController>().enabled = false;
        mage.GetComponent<Animator>().SetInteger("State", 3);
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
