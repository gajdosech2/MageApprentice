using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    public static int total_score = 0;
    public static int total_time = 0;

    public string level = "FirstLevel";
    public Text score;
    public Text time;

    void Start()
    {
        Invoke("Level", 7.5f);
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
    }

    void Level()
    {
        total_score = 0;
        total_time = 0;
        Application.LoadLevel(level);
    }
}
