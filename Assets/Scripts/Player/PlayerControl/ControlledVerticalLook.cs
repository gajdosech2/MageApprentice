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

    float rotationY, targetRotationY, defaultRotationY;

    bool isLocked = false;

    void Start()
    {
        defaultRotationY = targetRotationY = 20.0f;
    }

    void OnEnable()
    {
        rotationY = targetRotationY = transform.rotation.eulerAngles.x;
    }

    void UpdateTargetRotation()
    {
        //float axis_input = Input.GetAxis("Vertical") + joystick.Vertical + Input.GetAxis("Mouse Y");
        float axis_input = Input.GetAxis("Mouse Y");
        targetRotationY -= axis_input * sensitivity * Time.deltaTime;
        targetRotationY = Mathf.Clamp(targetRotationY, minTilt, maxTilt);
    }

    void Update()
    {
        if (!isLocked)
        {
            UpdateTargetRotation();
        }
        rotationY = Mathf.Lerp(rotationY, targetRotationY, smoothness);

        Vector3 rotation = transform.eulerAngles;
        rotation.x = rotationY;
        transform.eulerAngles = rotation;
    }

    public void SetLocked(bool is_locked)
    {
        isLocked = is_locked;
    }

    public void SetLockedToDefaultRotation(bool is_locked)
    {
        isLocked = is_locked;
        targetRotationY = defaultRotationY;
    }
}
