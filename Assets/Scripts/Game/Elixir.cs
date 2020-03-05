using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elixir : MonoBehaviour
{
    public GameObject message;
    public List<GameObject> elixirs;
    bool interact = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && interact)
        {
            for (Types i = Types.red; i <= Types.none; i++)
            {
                if (i == Types.none)
                {
                    Types drop = Elixirs.instance.Drop();
                    if (drop != Types.none)
                    {
                        elixirs[(int)drop].SetActive(true);
                    }
                }
                else if (elixirs[(int)i].active)
                {
                    elixirs[(int)i].SetActive(false);
                    Types pop = Elixirs.instance.Add(i);
                    if (pop != Types.none)
                    {
                        elixirs[(int)pop].SetActive(true);
                    }
                    break;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        message.SetActive(true);
        interact = true;
    }


    void OnTriggerExit(Collider other)
    {
        message.SetActive(false);
        interact = false;
    }
}
