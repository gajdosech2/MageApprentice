using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGUI : MonoBehaviour
{
    public GameObject hint;

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            hint.SetActive(false);
        }
    }
}
