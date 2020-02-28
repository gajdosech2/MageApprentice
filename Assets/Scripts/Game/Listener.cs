using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    public List<GameObject> to_activate = new List<GameObject>();
    public List<GameObject> to_hide = new List<GameObject>();
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Trigger()
    {
        foreach (GameObject activate in to_activate)
        {
            activate.SetActive(true);
        }

        foreach (GameObject hide in to_hide)
        {
            hide.SetActive(false);
        }

        if (animator != null) 
        {
            animator.SetInteger("State", 1);
        }
    }
}
