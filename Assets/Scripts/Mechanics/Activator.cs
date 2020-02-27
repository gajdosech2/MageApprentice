using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
	public UnityEvent OnActivate;
	public bool destroy = false;

	private Light light;
	private float deactivated_light;

	void Start()
	{
		light = GetComponentInChildren<Light>();
		if (light != null)
        {
			deactivated_light = light.intensity;
		}	
	}

	void OnTriggerEnter(Collider other)
	{
		OnActivate.Invoke();
		OnActivate.RemoveAllListeners();

		if (light != null)
        {
			light.intensity = 4;
			Invoke("Light", 10.0f);
		}

		if (destroy)
        {
			Destroy(gameObject);
        }
	}

	void Light()
	{
		light.intensity = deactivated_light;
	}
}
