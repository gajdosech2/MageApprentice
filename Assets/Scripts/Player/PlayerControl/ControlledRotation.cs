using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledRotation : MonoBehaviour
{
    public FixedJoystick joystick;

    public float sensitivity = 100.0f;

    public float smoothness = 0.5f;

    float rotationX = 0.0f;
    float targetRotationX = 0.0f;

    void Start()
    {
        rotationX = targetRotationX = transform.rotation.eulerAngles.y;
    }

    void OnEnable()
    {
        rotationX = targetRotationX = transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        float axis_input = Input.GetAxis("Horizontal") + joystick.Horizontal + Input.GetAxis("Mouse X");
        targetRotationX += axis_input * sensitivity * Time.deltaTime;

        float prev_rotation = rotationX;
        rotationX = Mathf.Lerp(targetRotationX, rotationX, smoothness);

        Vector3 rotation = transform.eulerAngles;
        rotation.y = rotationX;
        transform.eulerAngles = rotation;
    }
}
