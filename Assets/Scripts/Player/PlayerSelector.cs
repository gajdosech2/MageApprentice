using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
	void FixedUpdate()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
		if (Physics.Raycast(ray, out hit, 1000))
		{
	
		}
	}
}
