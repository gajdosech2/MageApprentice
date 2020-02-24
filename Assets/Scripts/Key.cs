using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
	public UnityEvent OnActivate;

	void OnTriggerEnter(Collider other)
	{
		OnActivate.Invoke();
		OnActivate.RemoveAllListeners();
	}
}
