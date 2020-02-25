using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool mouse = false;
    string axis;
    float move_speed = 5.0f;
    float rotation_speed = 110.0f;
    float jump_speed = 10.0f;
    float gravity = 17.0f;
    float distance_to_ground = 0.0f;

    Vector3 move_direction = Vector3.zero;
    CharacterController controller;
    Animator animator;
    Collider collider;

    void Start()
    {
        axis = mouse ? "Mouse X" : "Horizontal";
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        distance_to_ground = collider.bounds.extents.y;
    }

    bool IsAnimationGrounded()
    {
        if (controller.isGrounded)
        {
            return true;
        }

        RaycastHit hit;
        if (Physics.Raycast(collider.bounds.center, -Vector3.up, out hit, distance_to_ground + 0.4f))
        {
            return true;
        }

        return false;
    }

    void Animation()
    {
        if (IsAnimationGrounded() || Time.time < 0.5f) 
        {
            if (Mathf.Abs(move_direction.x) < 0.1f && Mathf.Abs(move_direction.z) < 0.1f)
            {
                animator.SetInteger("State", 0);
            }
            else
            {
                animator.SetInteger("State", 1);
            }
        } 
        else
        {
            animator.SetInteger("State", 2);
        }
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            move_direction.y = 0;
            if (Input.GetButton("Jump"))
            {
                move_direction.y += jump_speed;
            }
        }

        move_direction.x = 0;
        move_direction.z = 0;
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            move_direction.x = (controller.transform.forward * move_speed).x;
            move_direction.z = (controller.transform.forward * move_speed).z;
        }

        move_direction.y -= gravity * Time.deltaTime;
        controller.Move(move_direction * Time.deltaTime);

        controller.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * rotation_speed * Time.deltaTime);
        controller.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * rotation_speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Move();
        Animation();
    }
}
