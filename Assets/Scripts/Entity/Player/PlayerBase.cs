using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase: MonoBehaviour
{
    float aggro;
    float maxHealth;
    float currentHealth;
    float attackSpeed;
    int damage;

    MonsterBase target;
    public virtual void Attack()
    {
        Debug.Log("Player Attack");
    }
    public virtual void Hit()
    {
        Debug.Log("Player Hit");
    }
    public virtual void Die()
    {
        Debug.Log("Player Die");
    }
    public virtual void UseSkill()
    {
        Debug.Log("Using skill");
    }

    public void Init(float _maxHealth, float _attackSpeed, int _damage)
    {
        maxHealth = _maxHealth;
        currentHealth = maxHealth;
        attackSpeed = _attackSpeed;
        damage = _damage;
    }
}
