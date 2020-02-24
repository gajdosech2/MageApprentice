using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public string color = "";
    public ManagerPuzzle manager;
    private Light light;
    private float deactivated_light;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        deactivated_light = light.intensity;
    }

	void OnTriggerEnter(Collider other)
	{
		if (manager != null)
        {
            manager.Activate(color);
            light.intensity = deactivated_light + 4;
            Invoke("Light", 10.0f);
        }
	}

    void Light()
    {
        light.intensity = deactivated_light;
    }

}
