using System;
using UnityEngine;

[Serializable]
public class EnemyInfo
{
    [Header("Health")]
    public float enemyHealth = 100f;

    [Header("Settings")]
    public float enemySpeed;
    public float wanderSpeed;
    public float personalSpace;

    [Header("Attack Settings")]
    public int enemyDamage;
    public float bulletSpeed;
    public float attackDelay;
    public float enemyVision;
    public float attackRangeStart;
    public float attackRangeEnd;

    [Header("Bullet Prefab")]
    public GameObject bullet;
    public AudioClip shootSound;

    public void ApplyBuff(BuffStats buff)
    {
        enemyHealth += buff.lifeIncrease;
        enemyDamage += buff.damage;
        enemySpeed += buff.movementIncrease;
        enemyVision += buff.vision;

        attackDelay -= buff.weaponDelay;
        attackRangeStart -= buff.attackRange;
        attackRangeEnd += buff.attackRange;
    }

    public void ReverseBuff(BuffStats buff)
    {
        enemyHealth -= buff.lifeIncrease;
        enemyDamage -= buff.damage;
        enemySpeed -= buff.movementIncrease;
        enemyVision -= buff.vision;

        attackDelay += buff.weaponDelay;
        attackRangeStart += buff.attackRange;
        attackRangeEnd -= buff.attackRange;
    }
}
