using System;
using UnityEngine;

[Serializable]
public class WeaponEffect
{
    [Header("Deafult Settings")]
    public float probability = 0.1f;

    [Header("Bullet Changes")]
    public float bulletSpeedMultiplier = 0.5f;
    public float bulletDamageMultiplier = 0.5f;
    public float bulletLifeMultiplier = 1;
    public AudioClip weaponSound;

    [Header("Bullet Effect")]
    public BulletEffect effect;
}
