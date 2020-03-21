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
    RuntimeAnimatorController oldAnimatorController;
    Animator animator;
    PlayerInterface player;
    PlayerController playerController;
    CharacterController controller;

    float move_speed = 6.0f;
    float jump_speed = 6.0f;
    bool on_run = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerInterface>();
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        animator = GetComponent<Animator>();
        oldAnimatorController = animator.runtimeAnimatorController;
        playerController = GetComponent<PlayerController>();
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
            playerController.move_direction.y = 0;
            if (Input.GetButtonDown("Jump") || space.GetDown())
            {
                playerController.move_direction.y += jump_speed;
            }
        }

        playerController.move_direction.z = (controller.transform.forward * move_speed).z;
        playerController.move_direction.x = Input.GetAxis("Horizontal") * 5 + joystick.Horizontal * 5;
        controller.Move(playerController.move_direction * Time.deltaTime);
    }

    void Go()
    {
        on_run = true;
        player.RestorePlayerCamera();
    }

    void Done()
    {
        player.RestorePlayerCamera();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Run"))
        {
            player.SetControlsEnabled(false);
            playerController.move_direction = Vector3.zero;
            transform.rotation = Quaternion.identity;
            animator.runtimeAnimatorController = newAnimatorController;
            ball.enabled = true;

            player.SetTemporaryCamera(
                GameObject.Find("Camera Run Begin").GetComponent<Camera>());
            Invoke("Go", 4.0f);
        }
        else if (other.gameObject.name.Equals("End"))
        {
            other.gameObject.SetActive(false);
            GameObject.Find("Cell").GetComponent<Animator>().SetInteger("State", 1);
            on_run = false;
            player.SetControlsEnabled(true);
            animator.runtimeAnimatorController = oldAnimatorController;

            player.SetTemporaryCamera(GameObject.Find("Camera Run Done").GetComponent<Camera>());
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
