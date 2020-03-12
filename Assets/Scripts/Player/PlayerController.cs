using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public JoyButton menu;
    float distance_to_ground = 0.0f;

    [HideInInspector]
    public Vector3 rotation_vector = Vector3.zero;
    [HideInInspector]
    public Vector3 move_direction = Vector3.zero;

    CharacterController controller;
    Animator animator;
    Collider collider;
    RaycastHit hit;

    void Start()
    {
        if (QualitySettings.GetQualityLevel() < 1)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
        }
        //Cursor.visible = false;
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

        return Physics.Raycast(collider.bounds.center, -Vector3.up, out hit, distance_to_ground + 0.4f);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) || menu.GetDown())
        {
            ManagerGame.Menu();
        }
        Animation();
    }

    void OnDestroy()
    {
        Cursor.visible = true;
    }
}
