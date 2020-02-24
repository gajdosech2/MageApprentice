using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManagerPuzzle : MonoBehaviour
{
    public UnityEvent OnActivate;
    public List<Light> notifier_lights = new List<Light>();
    string sequence = "";

    public void Activate(string color)
    {
        if (sequence.Contains(color))
        {
            return;
        }
        Light light = notifier_lights[sequence.Length];
        sequence += color;
        switch (color)
        {
            case "R":
                light.color = Color.red;
                break;
            case "B":
                light.color = Color.blue;
                break;
            case "G":
                light.color = Color.green;
                break;
            case "Y":
                light.color = Color.yellow;
                break;
            case "P":
                light.color = Color.magenta;
                break;
        }
        Check();
    }

    void Check()
    {
        if (sequence.Length == 5)
        {
            if (sequence.Equals("RGYPB")) 
            {
                OnActivate.Invoke();
                OnActivate.RemoveAllListeners();
            }
            else 
            {
                sequence = "";
                foreach (Light light in notifier_lights)
                {
                    light.color = Color.white;
                }
            }
        }
    }
}
