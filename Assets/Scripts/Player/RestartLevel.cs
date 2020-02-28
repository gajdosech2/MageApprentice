using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    public string level = "FirstLevel";
    PlayerMovement player;

    void Start()
    {
        player = GameObject.Find("MageCharacter").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        player.enabled = false;
        if (Input.GetButton("Submit"))
        {
            Application.LoadLevel(level);
        }
    }
}
