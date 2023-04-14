using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float initHealth = 100;
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    public event Action OnDeath;

    //생명체가 활성화될 때 상태를 리셋
    protected virtual void OnEnable()
    {
        dead = false;
        health = initHealth;
    }
    //데미지 입는 기능
    public virtual void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        health -= damage;
        if (!dead && health < 0)
        {
            Die();
        }
    }
    //체력 회복 기능
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead) return;

        health += newHealth;
    }
    //사망처리
    public virtual void Die()
    {
        dead = true;
        OnDeath?.Invoke();
    }
}
