using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public string color = "";
    public GameObject hint;
    public GameObject read;
    public GameObject confirm;
    public JoyButton enter;

    private bool interact = false;
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
        interact = true;
		if (BooksPuzzle.instance != null)
        {  
            collider.size = new Vector3(collider.size.x, 5, collider.size.z);
        }
        if (hint != null)
        {
            hint.SetActive(true);
        }
        if (read != null)
        {
            read.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        interact = false;
        if (hint != null)
        {
            hint.SetActive(false);
        }
        if (read != null)
        {
            read.SetActive(false);
        }
        if (confirm != null)
        {
            confirm.SetActive(false);
        }
    }

    void Read()
    {
        BooksPuzzle.instance.Activate(color);
        light.intensity = deactivated_light + 4;
        Invoke("Light", 10.0f);
        read.SetActive(false);
        confirm.SetActive(true);
    }

    void Light()
    {
        light.intensity = deactivated_light;
    }

    void Update()
    {
        if (interact && (enter.GetDown() || Input.GetButtonDown("Submit")))
        {
            Read();
        }
    }

}
