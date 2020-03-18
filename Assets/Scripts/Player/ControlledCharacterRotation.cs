using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledCharacterRotation : MonoBehaviour
{
    public FixedJoystick joystick;

    public float sensitivity = 100.0f;

    public float smoothness = 0.5f;

    float rotationHorizontal = 0.0f;
    float targetRotationHorizontal = 0.0f;

    void Start()
    {
        rotationHorizontal = transform.rotation.y;
    }

    void Update()
    {
        //float axis_input = Input.GetAxis("Horizontal") + joystick.Horizontal + Input.GetAxis("Mouse X");
        float axis_input = Input.GetAxis("Mouse X");
        targetRotationHorizontal += axis_input * sensitivity * Time.deltaTime;

        float prev_rotation = rotationHorizontal;
        rotationHorizontal = Mathf.Lerp(targetRotationHorizontal, rotationHorizontal, smoothness);

        Vector3 rotation = transform.eulerAngles;
        rotation.y = rotationHorizontal;
        transform.eulerAngles = rotation;
    }
}
