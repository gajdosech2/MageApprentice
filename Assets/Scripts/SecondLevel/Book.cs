using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public string color = "";
    public ManagerPuzzle manager;
    public GameObject hint;

    private Light light;
    private float deactivated_light;
    private BoxCollider collider;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        light = GetComponentInChildren<Light>();
        deactivated_light = light.intensity;
    }

	void OnTriggerEnter(Collider other)
	{
		if (manager != null)
        {
            manager.Activate(color);
            light.intensity = deactivated_light + 4;
            collider.size = new Vector3(collider.size.x, 5, collider.size.z);
            Invoke("Light", 10.0f);
        }
        if (hint != null)
        {
            hint.SetActive(true);
        }
	}

    void OnTriggerExit(Collider other)
    {
        if (hint != null)
        {
            hint.SetActive(false);
        }
    }

    void Light()
    {
        light.intensity = deactivated_light;
    }

}
