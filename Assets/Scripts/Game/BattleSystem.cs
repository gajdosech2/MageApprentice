using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    public UnityEvent OnActivate;
    public BattleUnit enemy_unit;
    public GameObject enemy_exclamation;
    public GameObject over;

    private Vector3 old_offset;
    private float lerp = 0.0f;
    private float new_health;

    private CameraController camera;
    private BattleUnit player_unit;
    private Transform player_transform;
    private PlayerController player_controller;
    private PlayerMovement player_movement;
    private Animator player_animator;

    public GameObject gui;
    public string enemy_name;
    public Text text;
    public Slider player_health;
    public Slider enemy_health;
    public GameObject player_gui;

    void Start()
    {
        GameObject player = GameObject.Find("MageCharacter");
        player_transform = player.transform;
        player_movement = player.GetComponent<PlayerMovement>();
        player_controller = player.GetComponent<PlayerController>();
        player_unit = player.GetComponent<BattleUnit>();
        player_animator = player.GetComponent<Animator>();
        camera = Camera.main.GetComponent<CameraController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && lerp == 0.0f)
        {
            StartCoroutine("SetupBattle");
        }
    }

    IEnumerator SetupBattle()
    {
        player_controller.move_direction = Vector3.zero;
        player_movement.enabled = false;
        player_controller.transform.rotation = transform.rotation;
        old_offset = camera.offset;
        Vector3 new_offset = camera.offset - new Vector3(2.0f, 0.25f, 0.0f);
        enemy_exclamation.SetActive(true);
        while (lerp < 1.0f)
        {
            lerp += 0.5f * Time.deltaTime;
            camera.offset = Vector3.Lerp(old_offset, new_offset, lerp);
            yield return null;
        }
        enemy_exclamation.SetActive(false);
        gui.SetActive(true);
        Cursor.visible = true;
        enemy_health.value = enemy_health.maxValue;
        text.text = "Wild " + enemy_name + " appeared!";
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        text.gameObject.SetActive(false);
        player_controller.enabled = false;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        player_gui.SetActive(true);
    }

    public void OnAttackButton()
    {
        StartCoroutine("PlayerAttack");
    }

    IEnumerator PlayerAttack()
    {
        player_gui.SetActive(false);
        text.text = "The attack is successful!";
        text.gameObject.SetActive(true);
        player_animator.SetInteger("State", 4);
        yield return new WaitForSeconds(0.5f);
        player_animator.SetInteger("State", 0);
        for (int i = 0; i < 3; i++)
        {
            enemy_health.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.15f);
            enemy_health.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.15f);
        }
        new_health = Mathf.Max(0, enemy_health.value - 3);
        while (enemy_health.value > new_health)
        {
            enemy_health.value -= 0.1f;
            yield return null;
        }
        enemy_health.value = new_health;
        bool dead = enemy_health.value <= 0;
        yield return new WaitForSeconds(1.5f);
        text.gameObject.SetActive(false);
        if (dead)
        {
            StartCoroutine("LeaveBattle");
        }
        else
        {
            StartCoroutine("EnemyTurn");
        } 
    }

    public void OnHealButton()
    {
        StartCoroutine("PlayerHeal");
    }

    IEnumerator PlayerHeal()
    {
        player_gui.SetActive(false);
        text.text = "The player is regaining health!";
        text.gameObject.SetActive(true);
        player_animator.SetInteger("State", 6);
        yield return new WaitForSeconds(0.5f);
        new_health = Mathf.Min(player_health.value + 6, player_health.maxValue);
        while (player_health.value < new_health)
        {
            player_health.value += 0.1f;
            yield return null;
        }
        player_health.value = new_health;
        player_animator.SetInteger("State", 0);
        yield return new WaitForSeconds(1.5f);
        text.gameObject.SetActive(false); 
        StartCoroutine("EnemyTurn");
    }

    IEnumerator EnemyTurn()
    {
        text.text = enemy_name + " attacks!";
        text.gameObject.SetActive(true);
        player_animator.SetInteger("State", 5);
        yield return new WaitForSeconds(0.5f);
        player_animator.SetInteger("State", 0);
        for (int i = 0; i < 3; i++)
        {
            player_health.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.15f);
            player_health.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.15f);
        }
        new_health = Mathf.Max(0, player_health.value - 3);
        while (player_health.value > new_health)
        {
            player_health.value -= 0.1f;
            yield return null;
        }
        player_health.value = new_health;
        bool dead = player_health.value <= 0;
        yield return new WaitForSeconds(1.5f); 
        if (dead)
        {
            over.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
            PlayerTurn();
        }
    }

    IEnumerator LeaveBattle()
    {
        text.text = "Battle Ends!";
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        player_movement.enabled = true;
        player_controller.enabled = true;
        camera.offset = old_offset;
        gui.SetActive(false);
        Cursor.visible = false;
        Destroy(enemy_unit.gameObject);
        OnActivate.Invoke();
    }
}
