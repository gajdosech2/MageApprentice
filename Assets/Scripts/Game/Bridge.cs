using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    Animator animator;
    public GameObject smoke;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void Activate()
    {
        animator.enabled = true;
        if (smoke != null)
        {
            smoke.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
