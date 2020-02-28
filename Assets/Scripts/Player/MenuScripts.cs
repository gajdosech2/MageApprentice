using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScripts : MonoBehaviour
{
    public GameObject menu;
    public GameObject challenges;

    public void MainMenu()
    {
        menu.SetActive(true);
        challenges.SetActive(false);
    }

    public void Challenges()
    {
        challenges.SetActive(true);
        menu.SetActive(false);
    }

    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
