using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledVerticalLook : MonoBehaviour
{
    public FixedJoystick joystick;

    public float sensitivity = 100.0f;
    public float smoothness = 0.5f;
    public float minTilt = -70.0f;
    public float maxTilt = 70.0f;

    float rotationVertical = 0.0f;
    float targetRotationVertical = 0.0f;

    void Start()
    {
        rotationVertical = transform.rotation.x;
    }

    void Update()
    {
        //float axis_input = Input.GetAxis("Vertical") + joystick.Vertical + Input.GetAxis("Mouse Y");
        float axis_input = Input.GetAxis("Mouse Y");
        targetRotationVertical -= axis_input * sensitivity * Time.deltaTime;
        targetRotationVertical = Mathf.Clamp(targetRotationVertical, minTilt, maxTilt);

        float prev_rotation = rotationVertical;
        rotationVertical = Mathf.Lerp(targetRotationVertical, rotationVertical, smoothness);

        Vector3 rotation = transform.eulerAngles;
        rotation.x = rotationVertical;
        transform.eulerAngles = rotation;
    }
}
