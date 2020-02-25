using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    bool has_requiremenet = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Done()
    {
        has_requiremenet = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (has_requiremenet)
        {
            animator.SetInteger("State", 1);
        }
    }
}
