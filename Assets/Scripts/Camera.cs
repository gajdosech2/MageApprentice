using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public PlayerController player;
    private Vector3 offset;
    float distance;

    void Start()
    {
        offset = target.transform.position - transform.position;
        distance = offset.magnitude;
    }

    void Update()
    {
        Quaternion rotation = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform.position + new Vector3(0, 1.15f, 0));

        RaycastHit hit;
        player.rotation_disable = Physics.Raycast(target.transform.position, transform.position - target.transform.position, out hit, distance);
    }
}
