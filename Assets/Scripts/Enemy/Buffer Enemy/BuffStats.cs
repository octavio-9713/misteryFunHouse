using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuffStats
{
    [Header("Life")]
    public int lifeIncrease = 25;

    [Header("Movement")]
    public float movementIncrease = 1.15f;
    public float vision = 1.5f;

    [Header("Weapon")]
    public int damage = 2;
    public float weaponDelay = 0.5f;
    public float attackRange = 1.15f;
}
