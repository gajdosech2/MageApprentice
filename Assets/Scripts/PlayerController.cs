using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool mouse = false;
    public bool rotation_disable = false;

    string axis;
    float move_speed = 5.0f;
    float rotation_speed = 80.0f;
    float jump_speed = 11.0f;
    float gravity = 17.0f;
    float distance_to_ground = 0.0f;

    Vector3 move_direction = Vector3.zero;
    CharacterController controller;
    Animator animator;
    Collider collider;

    void Start()
    {
        axis = mouse ? "Mouse X" : "Horizontal";
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        distance_to_ground = collider.bounds.extents.y;
    }

    bool IsGrounded()
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

    void DetermineState()
    {
        if (IsGrounded() || Time.time < 0.5f) 
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
        if (IsGrounded())
        {
            move_direction = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                move_direction = controller.transform.forward * move_speed;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                move_direction.y = jump_speed;
            }
        }

        move_direction.y -= gravity * Time.deltaTime;
        controller.Move(move_direction * Time.deltaTime);

        if (!rotation_disable)
        {
            controller.transform.Rotate(new Vector3(0, Input.GetAxis(axis), 0) * rotation_speed * Time.deltaTime);
        }
    }

    void Update()
    {
        Move();
        DetermineState();
    }
}
