using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    private Vector3 old_offset;
    private float lerp = 0.0f;
    private float new_health;

    private PlayerInterface player;

    private PlayerController player_controller;
    private Animator player_animator;

    private Camera battle_camera;
    private Animator battle_camera_animator;

    public UnityEvent OnActivate;
    public BattleUnit enemy_unit;
    public GameObject enemy_exclamation;
    public GameObject over;

    public string enemy_name;
    public GameObject gui;
    public GameObject player_gui;
    public Text text;
    public Button attack_button;
    public Button heal_button;
    public Slider player_health;
    public Slider enemy_health;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerInterface>();

        player_controller = player.character.GetComponent<PlayerController>();
        player_animator = player.character.GetComponent<Animator>();

        battle_camera = GetComponentInChildren<Camera>();
        battle_camera_animator = battle_camera.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && lerp == 0.0f)
        {
            attack_button.onClick.RemoveAllListeners();
            attack_button.onClick.AddListener(OnAttackButton);
            heal_button.onClick.RemoveAllListeners();
            heal_button.onClick.AddListener(OnHealButton);

            StartCoroutine("SetupBattle");
        }
    }

    IEnumerator SetupBattle()
    {
        player.SetControlsEnabled(false);

        player_controller.move_direction = Vector3.zero;
        player.character.transform.rotation = transform.rotation;
        player.character.transform.position = transform.Find("Stepper Player").transform.position;

        enemy_exclamation.SetActive(true);

        player.SetTemporaryCamera(battle_camera);
        battle_camera_animator.SetTrigger("BattleStart");

        yield return new WaitForSeconds(2.0f);

        enemy_exclamation.SetActive(false);
        gui.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

        player.RestorePlayerCamera();
        player.SetControlsEnabled(true);
        player_controller.enabled = true;
        gui.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(enemy_unit.gameObject);
        OnActivate.Invoke();
    }
}
