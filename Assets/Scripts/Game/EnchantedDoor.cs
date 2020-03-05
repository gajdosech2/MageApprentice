using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantedDoor : MonoBehaviour
{
    public Types type = Types.none;
    public Collider collider;
    public GameObject powerful;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (powerful != null)
            {
                if (Elixirs.instance.player_slot1 == type && Elixirs.instance.player_slot2 == type)
                {
                    collider.enabled = false;
                    
                }
                else
                {
                    powerful.SetActive(true);
                    collider.enabled = true;
                }
            }
            else if (Elixirs.instance.player_slot1 == type || Elixirs.instance.player_slot2 == type)
            {
                collider.enabled = false;
            }
            else
            {
                collider.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (powerful != null)
        {
            powerful.SetActive(false);
        }
    }
}
