using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
	public UnityEvent OnActivate;

	void OnTriggerEnter(Collider other)
	{
		OnActivate.Invoke();
		OnActivate.RemoveAllListeners();
		Light light = GetComponentInChildren<Light>();
		light.intensity = 4;
	}
}
