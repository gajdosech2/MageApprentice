using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform.position + new Vector3(0, 1, 0));
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hello World");
    }
}
