using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    GameObject info;
    Animator animator;

    void Start()
    {
        info = transform.Find("Info").gameObject;
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (info != null)
        {
            info.SetActive(true);
        }
        if (animator != null)
        {
            animator.SetInteger("State", 1);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (info != null)
        {
            info.SetActive(false);
        }
    }
}
