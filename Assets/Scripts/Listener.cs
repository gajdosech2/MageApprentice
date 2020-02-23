using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    public List<GameObject> to_activate = new List<GameObject>();
    public List<GameObject> to_hide = new List<GameObject>();

    public void ListenerTrigger()
    {
        foreach (GameObject activate in to_activate)
        {
            activate.SetActive(true);
        }

        foreach (GameObject hide in to_hide)
        {
            hide.SetActive(false);
        }
    }
}
