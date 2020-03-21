using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public JoyButton space;

    public float moveSpeed = 5.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = 17.0f;

    public float sensitivityHor = 100.0f;
    public float sensitivityVer = 100.0f;

    public float smoothnessHor = 0.5f;
    public float smoothnessVer = 0.5f;

    public float minTilt = -70.0f;
    public float maxTilt = 70.0f;

    CharacterController controller;
    PlayerController player;

    float rotationVertical = 0.0f;
    float rotationHorizontal = 0.0f;
    float targetRotationVertical = 0.0f;
    float targetRotationHorizontal = 0.0f;

    Transform eye;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<PlayerController>();
        eye = transform.Find("Eye");

        rotationHorizontal = player.transform.rotation.y;
        rotationVertical = eye.rotation.x;
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            player.move_direction.y = 0;
            if (Input.GetButtonDown("Jump") || space.GetDown())
            {
                player.move_direction.y += jumpSpeed;
            }
        }

        player.move_direction.x = 0;
        player.move_direction.z = 0;
        if (Input.GetAxisRaw("Vertical") > 0 || joystick.Vertical > 0)
        {
            player.move_direction.x = (controller.transform.forward * moveSpeed).x;
            player.move_direction.z = (controller.transform.forward * moveSpeed).z;
        }

        player.move_direction.y -= gravity * Time.deltaTime;

        controller.Move(player.move_direction * Time.deltaTime);
    }

    void Rotate()
    {
        float axis_input = Input.GetAxis("Horizontal") + joystick.Horizontal + Input.GetAxis("Mouse X");
        targetRotationHorizontal += axis_input * sensitivityHor * Time.deltaTime;

        float prev_rotation = rotationHorizontal;
        rotationHorizontal = Mathf.Lerp(targetRotationHorizontal, rotationHorizontal, smoothnessHor);
        float delta_rotation = rotationHorizontal - prev_rotation;

        player.rotation_vector.y = delta_rotation;

        Vector3 rotation = transform.eulerAngles;
        rotation.y = rotationHorizontal;
        transform.eulerAngles = rotation;
    }

    void Tilt()
    {
        //float axis_input = Input.GetAxis("Vertical") + joystick.Vertical + Input.GetAxis("Mouse Y");
        float axis_input = Input.GetAxis("Mouse Y");
        targetRotationVertical -= axis_input * sensitivityVer * Time.deltaTime;
        targetRotationVertical = Mathf.Clamp(targetRotationVertical, minTilt, maxTilt);

        float prev_rotation = rotationVertical;
        rotationVertical = Mathf.Lerp(targetRotationVertical, rotationVertical, smoothnessVer);

        float delta_rotation = rotationVertical - prev_rotation;

        player.rotation_vector.x = delta_rotation;

        Vector3 rotation = eye.eulerAngles;
        rotation.x = rotationVertical;
        eye.eulerAngles = rotation;
    }

    void Update()
    {
        Move();
        Rotate();
        Tilt();
    }
}
