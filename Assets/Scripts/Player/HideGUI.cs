using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGUI : MonoBehaviour
{
    public JoyButton enter;
    public GameObject hint;

    void Update()
    {
        if (Input.GetButtonDown("Submit") || enter.GetDown())
        {
            hint.SetActive(false);
        }
    }
}
