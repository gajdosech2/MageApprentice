using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject info;

    void OnTriggerEnter(Collider other)
    {
        if (info != null)
        {
            info.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (info != null)
        {
            info.SetActive(false);
        }
    }
}
