using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;

    public void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0, 0, 50);
    }

    void FixedUpdate()
    {
        rb.AddForce(0, 0, 50 * Time.deltaTime);
    }
}
