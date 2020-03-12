using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public JoyButton space;
    public RuntimeAnimatorController newAnimatorController;
    public GameObject over;

    Ball ball;
    CameraController camera;
    RuntimeAnimatorController oldAnimatorController;
    Animator animator;
    PlayerMovement normal_movement;
    PlayerController player;
    CharacterController controller;

    float move_speed = 6.0f;
    float jump_speed = 6.0f;
    bool on_run = false;

    void Start()
    {
        camera = Camera.main.GetComponent<CameraController>();
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        animator = GetComponent<Animator>();
        oldAnimatorController = animator.runtimeAnimatorController;
        normal_movement = GetComponent<PlayerMovement>();
        player = GetComponent<PlayerController>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (on_run)
        {
            Move();
        }
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            player.move_direction.y = 0;
            if (Input.GetButtonDown("Jump") || space.GetDown())
            {
                player.move_direction.y += jump_speed;
            }
        }

        player.move_direction.z = (controller.transform.forward * move_speed).z;
        player.move_direction.x = Input.GetAxis("Horizontal") * 5 + joystick.Horizontal * 5; 
        controller.Move(player.move_direction * Time.deltaTime);
    }

    void Go()
    {
        on_run = true;
        camera.target = transform;
    }

    void Done()
    {
        camera.target = transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Run"))
        {
            normal_movement.enabled = false;
            player.move_direction = Vector3.zero;
            transform.rotation = Quaternion.identity;
            animator.runtimeAnimatorController = newAnimatorController;
            ball.enabled = true;
            camera.target = GameObject.Find("CameraTargetOne").transform;
            Invoke("Go", 4.0f);
        }
        else if (other.gameObject.name.Equals("End"))
        {
            other.gameObject.SetActive(false);
            GameObject.Find("Cell").GetComponent<Animator>().SetInteger("State", 1);
            on_run = false;
            normal_movement.enabled = true;
            animator.runtimeAnimatorController = oldAnimatorController;
            camera.target = GameObject.Find("CameraTargetTwo").transform;
            Invoke("Done", 4.0f);
        }
        else if (other.gameObject.layer == 9)
        {
            over.SetActive(true);
            on_run = false;
            ball.Stop();
        }
    }
}
