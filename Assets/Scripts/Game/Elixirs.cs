using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Types { red, green, blue, white, none };

public class Elixirs : MonoBehaviour
{
    public static Elixirs instance;
    public List<GameObject> slot1_elixirs;
    public List<GameObject> slot2_elixirs;
    Types player_slot1 = Types.none;
    Types player_slot2 = Types.none;

    void Refresh()
    {
        for (Types i = Types.red; i < Types.none; i++)
        {
            if (player_slot1 == i)
            {
                slot1_elixirs[(int)i].SetActive(true);
            }
            else
            {
                slot1_elixirs[(int)i].SetActive(false);
            }

            if (player_slot2 == i)
            {
                slot2_elixirs[(int)i].SetActive(true);
            }
            else
            {
                slot2_elixirs[(int)i].SetActive(false);
            }
        }
    }

    public Types Drop()
    {
        Types ret = Types.none;
        if (player_slot2 != Types.none)
        {
            ret = player_slot2;
            player_slot2 = Types.none;
        }
        else if (player_slot1 != Types.none)
        {
            ret = player_slot1;
            player_slot1 = Types.none;
        }
        return ret;
    }

    public Types Add(Types elixir)
    {
        if (player_slot1 == Types.none)
        {
            player_slot1 = elixir;
            return Types.none;
        }
        if (player_slot2 == Types.none)
        {
            player_slot2 = player_slot1;
            player_slot1 = elixir;
            return Types.none;
        }
        Types pop = player_slot2;
        player_slot2 = player_slot1;
        player_slot1 = elixir;
        return pop;
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        Refresh();
    }

}
