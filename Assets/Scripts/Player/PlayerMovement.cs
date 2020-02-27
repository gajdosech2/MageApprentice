using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float move_speed = 5.0f;
    float rotation_speed = 110.0f;
    float jump_speed = 10.0f;
    float gravity = 17.0f;

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
            if (Input.GetButtonDown("Jump"))
            {
                player.move_direction.y += jump_speed;
            }
        }

        player.move_direction.x = 0;
        player.move_direction.z = 0;
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            player.move_direction.x = (controller.transform.forward * move_speed).x;
            player.move_direction.z = (controller.transform.forward * move_speed).z;
        }

        player.move_direction.y -= gravity * Time.deltaTime;

        controller.Move(player.move_direction * Time.deltaTime);

        player.rotation_vector.y = Input.GetAxis("Mouse X") + Input.GetAxis("Horizontal");
        controller.transform.Rotate(player.rotation_vector * rotation_speed * Time.deltaTime);
    }

    void Update()
    {
        Move();
    }
}
