using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    public Transform character;
    public Camera camera;

    ControlledMovement movementControl;
    ControlledRotation rotationControl;
    ControlledVerticalLook verticalViewControl;
    Camera temporaryCamera;

    public void Start()
    {
        character = transform.Find("MageCharacter");
        camera = GetComponentInChildren<Camera>();

        movementControl = GetComponentInChildren<ControlledMovement>();
        rotationControl = GetComponentInChildren<ControlledRotation>();
        verticalViewControl = GetComponentInChildren<ControlledVerticalLook>();
    }

    public void SetControlsEnabled(bool enabled, bool lockCam = true)
    {
        movementControl.enabled = enabled;
        rotationControl.enabled = enabled;
        if (lockCam)
        {
            verticalViewControl.SetLockedToDefaultRotation(!enabled);
        }
        else
        {
            verticalViewControl.SetLocked(!enabled);
        }
    }

    public void SetTemporaryCamera(Camera new_camera)
    {
        camera.enabled = false;
        if (temporaryCamera != null)
        {
            temporaryCamera.enabled = false;
        }
        new_camera.enabled = true;
        temporaryCamera = new_camera;
    }

    public void RestorePlayerCamera()
    {
        if (temporaryCamera != null)
        {
            temporaryCamera.enabled = false;
            temporaryCamera = null;
        }
        camera.enabled = true;
    }
}
