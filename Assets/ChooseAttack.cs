using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAttack : MonoBehaviour
{
    public Transform player;
    public float attackDistance = 7.5f;

    private QLearning.AttackMechanics _attackMechanics;

    private Dictionary<int, int> _attackSteps;
    private Animator _animator;
    private FightHealth _fightHealth;

    void Start()
    {
        _attackMechanics = new QLearning.AttackMechanics(GetComponent<Transform>());
        _animator = GetComponent<Animator>();
        _fightHealth = GameObject.FindWithTag("scene").GetComponent<FightHealth>();
        StartCoroutine(CooldownAttack());
    }

    private QLearning.AttackMechanics.HealthChange PerformAttack(float damage)
    {
        _animator.Play("WK_heavy_infantry_08_attack_B");
        float before = _fightHealth.InflictDamageOnPlayer(damage);
        float after = before - damage;
        return new QLearning.AttackMechanics.HealthChange(before, after);
    }

    IEnumerator CooldownAttack()
    {
        yield return new WaitForSeconds(_attackMechanics.cooldown / 1000);
        _attackMechanics.Attack(player, _fightHealth.playerHealth, attackDistance, PerformAttack);

        StartCoroutine(CooldownAttack());
    }
}