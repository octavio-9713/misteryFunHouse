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
    public int attackDelay;
    public float enemyVision;
    public float attackRangeStart;
    public float attackRangeEnd;
}
