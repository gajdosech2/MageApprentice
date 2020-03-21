using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public JoyButton enter;
    public JoyButton R;
    public GameObject Lock;
    public GameObject Interact;

    float move_lerp = 1.0f;
    Vector3 start_position = Vector3.zero;
    Vector3 dir = Vector3.zero;
    GameObject player = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Interact.SetActive(true);
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Interact.SetActive(false);
        }
    }

    bool Free()
    {
        Vector3 pos = start_position + dir + Vector3.up/4;
        return Physics.OverlapSphere(pos, 0.1f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore).Length == 0;
    }

    void Push()
    {
        if (player == null)
        {
            return;
        }
        Vector3 pos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        if (dir == Vector3.zero && Interact.activeSelf && !Lock.activeSelf && Vector3.Distance(pos, transform.position) < 0.98f)
        {
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

    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.R) || R.GetDown())
        {
            ManagerGame.total_score = Mathf.Max(ManagerGame.total_score - GameObject.Find("MageCharacter").GetComponent<PlayerCollector>().level_score, 0);
            Application.LoadLevel("Level06");
        }

        if (Interact.activeSelf && dir == Vector3.zero && (Input.GetButtonDown("Submit") || enter.GetDown()))
        {
            Lock.SetActive(!Lock.activeSelf);
        }

        Push();

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
