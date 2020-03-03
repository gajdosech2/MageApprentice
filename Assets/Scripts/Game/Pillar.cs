using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    float move_lerp = 1.0f;
    Vector3 original_position = Vector3.zero;
    Vector3 start_position = Vector3.zero;
    Vector3 dir = Vector3.zero;

    void Start()
    {
        original_position = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && dir == Vector3.zero)
        {
            Vector3 pos = other.bounds.center;
            if (transform.position.x - pos.x > 0.8f)
            {
                dir = Vector3.right;
            }
            else if (transform.position.x - pos.x < -0.8f)
            {
                dir = Vector3.left;
            }
            else if (transform.position.z - pos.z > 0.8f)
            {
                dir = Vector3.forward;
            }
            else if (transform.position.z - pos.z < -0.8f)
            {
                dir = Vector3.back;
            }
        }
    }

    bool Free()
    {
        Vector3 pos = start_position + dir + Vector3.up;
        Collider[] colliders = Physics.OverlapSphere(pos, 0.1f);
        return colliders.Length == 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = original_position;
        }

        if (move_lerp < 1.0f)
        {
            move_lerp += 2.0f * Time.deltaTime;
            transform.position = Vector3.Lerp(start_position, start_position + dir, move_lerp);
        }
        else if (move_lerp >= 1.0f)
        {
            start_position = transform.position;
            if (Free())
            {
                move_lerp = 0.0f;
            }
            else
            {
                dir = Vector3.zero;
            }
        }
    }

}
