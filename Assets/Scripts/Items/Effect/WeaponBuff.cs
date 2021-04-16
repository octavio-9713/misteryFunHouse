using System;
using UnityEngine;

[Serializable]
public class WeaponBuff
{
    [Header("Bullet Prefab")]
    public GameObject bullet;
    public float bulletSpeed = 1.5f;
    public float bulletLife = 1.25f;

    [Header("Weapon Sound")]
    public AudioClip weaponSound;

    [Header("Weapon Settings")]
    public int bulletQuantity;
    public float weaponCadence = 1.5f;
    public float weaponCooldown = 1.2f;
    public float weaponDamage = 1.5f;
    public float weaponRecoil = 1.25f;

}
