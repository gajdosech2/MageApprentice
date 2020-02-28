using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advancer : MonoBehaviour
{
	public string level = "FirstLevel";

	void OnTriggerEnter(Collider other)
	{
		Application.LoadLevel(level);
	}
}
