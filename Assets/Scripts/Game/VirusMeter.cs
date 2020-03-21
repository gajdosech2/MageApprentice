using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusMeter : MonoBehaviour
{
    public GameObject over;
    public Slider slider;
    
    void Start()
    {
        slider.value = 0;
    }

    void Update()
    {
        slider.value += Time.deltaTime * 0.75f;
        if (slider.value >= slider.maxValue)
        {
            over.SetActive(true);
        }
    }

}
