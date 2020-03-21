using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTask : MonoBehaviour
{
    public GameObject message;
    public Text text;

    public string success;
    public string fail;

    public GameObject toActivate;
    public GameObject toHide;
    public CraftingSystem system;
    public Piece requirement;
    public Piece reward;

    private bool done = false;

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            message.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !done)
        {
            if (system.HasItem(requirement))
            {
                text.text = success;
                system.AddItem(reward);
                if (toActivate != null)
                {
                    toActivate.SetActive(true);
                }
                if (toHide != null)
                {
                    toHide.SetActive(false);
                }
                done = true;
            }
            else
            {
                text.text = fail;
            }
            message.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        message.SetActive(false);
    }
}
