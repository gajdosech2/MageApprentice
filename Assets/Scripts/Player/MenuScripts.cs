using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    public GameObject menu;
    public GameObject challenges;
    public GameObject settings;

    public void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

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

    public void Settings()
    {
        settings.SetActive(true);
        menu.SetActive(false);
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Setting(int level)
    {
        QualitySettings.SetQualityLevel(level, true);
        menu.SetActive(true);
        settings.SetActive(false);
    }
}
