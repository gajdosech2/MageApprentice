using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public JoyButton space;

    public float moveSpeed = 5.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = 17.0f;

    CharacterController controller;
    PlayerController player;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<PlayerController>();
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

    void Update()
    {
        Move();
    }
}
