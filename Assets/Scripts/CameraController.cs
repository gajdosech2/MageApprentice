using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    float distance;

    void Start()
    {
        offset = target.transform.position - transform.position;
        distance = offset.magnitude;
    }

    void Update()
    {
        Vector3 target_position = target.transform.position + new Vector3(0, 1.2f, 0);
        Quaternion rotation = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target_position);

        RaycastHit hit;
        if (Physics.Raycast(target_position, transform.position - target_position, out hit, distance))
        {
            transform.position = hit.point;
            transform.position -= (transform.position - target_position) * 0.1f;
        }
    }
}
