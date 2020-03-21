using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusCure : MonoBehaviour
{
    public Slider slider;
    public float offset;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            slider.value = 0; 
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(-70, offset + Time.time * 50, 0);
    }
}
