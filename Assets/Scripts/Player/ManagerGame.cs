using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    public static int total_score = 0;
    public static int total_time = 0;
    static int last_time = 0;

    public Text score;
    public Text time;

    public static void Menu()
    {
        Register();
        total_score = 0;
        Application.LoadLevel("Menu");
    }

    static void Register()
    {
        total_time = (int)Time.time - last_time;
        last_time = (int)Time.time;
    }

    void Start()
    {
        Register();
    }

    void Update()
    {
        if (score != null)
        {
            score.text = total_score.ToString();
        }
        if (time != null)
        {
            time.text = total_time.ToString() + "s";
        }
        if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.M) || Input.GetMouseButtonDown(0))
        {
            Menu();
        }
    }

}
