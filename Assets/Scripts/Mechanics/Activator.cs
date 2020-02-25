using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
	public UnityEvent OnActivate;
	public bool destroy = false;

	void OnTriggerEnter(Collider other)
	{
		OnActivate.Invoke();
		OnActivate.RemoveAllListeners();

		Light light = GetComponentInChildren<Light>();
		if (light != null)
        {
			light.intensity = 4;
		}

		if (destroy)
        {
			Destroy(gameObject);
        }
	}
}
