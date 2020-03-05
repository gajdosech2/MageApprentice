using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public GameObject over;
    public float delay = 2.0f;
    public List<Vector3> positions;

    int current_position = 0;
    float lerp = 1.0f;
    float rotate_lerp = 0.0f;
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = positions[current_position];
        current_position = positions.Count - 1;
        transform.LookAt(positions[1 % positions.Count]);
        animator.SetInteger("State", 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            over.SetActive(true);
            lerp = -1.0f;
            animator.SetInteger("State", 2);
            GameObject.Find("MageCharacter").GetComponent<Animator>().SetInteger("State", 3);
        }
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad < delay || lerp < 0.0f)
        {
            return;
        }

        if (lerp < 1.0f)
        {
            if (animator.GetInteger("State") == 1)
            {
                lerp += 0.13f * Time.deltaTime;
                int next_position = (current_position + 1) % positions.Count;
                transform.position = Vector3.Lerp(positions[current_position], positions[next_position], lerp);
            }
            else
            {
                lerp += 0.5f * Time.deltaTime;
            }
        }
        else
        {
            if (animator.GetInteger("State") == 1)
            {
                animator.SetInteger("State", 0);
            }
            else
            {
                current_position = (current_position + 1) % positions.Count;
                animator.SetInteger("State", 1);
                StartCoroutine("Rotate");
            }
            lerp = 0.0f;
        }
    }

    IEnumerator Rotate()
    {
        rotate_lerp = 0.0f;
        Quaternion start_rotation = transform.rotation;
        int next_position = (current_position + 1) % positions.Count;
        transform.LookAt(positions[next_position]);
        Quaternion target_rotation = transform.rotation;
        while (rotate_lerp < 1.0f)
        {
            rotate_lerp += 2.0f * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(start_rotation, target_rotation, rotate_lerp);
            yield return null;
        }
    }
}
