using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleInputNamespace;
using UnityEngine.UI;

public class PlayerFightBehaviour : MonoBehaviour
{
    public Transform enemy;
    public float attackDistance = 7.5f;
    public Dpad dpad;
    public Button attackBtn;

    private QLearning.AttackMechanics _attackMechanics;
    private Animator _animator;
    private FightHealth _fightHealth;
    private bool _ableToAttack;

    void Start()
    {
        _attackMechanics = new QLearning.AttackMechanics(GetComponent<Transform>());
        _animator = GetComponent<Animator>();
        _fightHealth = GameObject.FindWithTag("scene").GetComponent<FightHealth>();

        _ableToAttack = true;
        attackBtn.onClick.AddListener(delegate{
            if (_ableToAttack)
            {
                _animator.Play("WK_heavy_infantry_08_attack_B");
                _attackMechanics.Attack(enemy, _fightHealth.playerHealth, attackDistance, PerformAttack);
            }
        });
    }

    void Update()
    {
        HandleInteraction();
    }

    private QLearning.AttackMechanics.HealthChange PerformAttack(float damage)
    {
        float before = _fightHealth.InflictDamageOnEnemy(damage);
        float after = before - damage;
        StartCoroutine(ReloadCooldown());
        return new QLearning.AttackMechanics.HealthChange(before, after);
    }

    IEnumerator ReloadCooldown()
    {
        _ableToAttack = false;
        yield return new WaitForSeconds(_attackMechanics.cooldown / 1000);
        _ableToAttack = true;
    }

    private void HandleInteraction()
    {
        if (Input.GetKey(KeyCode.D) || dpad.xAxis.value == 1)
        {
            transform.Translate(0.05f, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.A) || dpad.xAxis.value == -1)
        {
            transform.Translate(-0.05f, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.S) || dpad.yAxis.value == -1)
        {
            transform.Translate(0f, 0f, -0.05f);
        }

        if (Input.GetKey(KeyCode.W) || dpad.yAxis.value == 1)
        {
            transform.Translate(0f, 0f, 0.05f);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && dpad.yAxis.value == 0 && dpad.xAxis.value == 0)
        {
            transform.Translate(0f, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.Space) && _ableToAttack)
        {
            _animator.Play("WK_heavy_infantry_08_attack_B");
            _attackMechanics.Attack(enemy, _fightHealth.playerHealth, attackDistance, PerformAttack);
        }
    }
}