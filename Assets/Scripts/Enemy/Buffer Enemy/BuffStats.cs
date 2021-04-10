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
    public float movementIncrease = 2000;
    public float vision = 10;

    [Header("Weapon")]
    public int damage = 1;
    public float weaponDelay = 0.5f;
    public float attackRange = 10f;
}
