using System;
using UnityEngine;

[Serializable]
public class WeaponInfo
{
    [Header ("Bullet Prefab")]
    public GameObject bullet;
    public float bulletSpeed = 8000f;
    public float bulletLife = 1f;

    [Header("Weapon Sound")]
    public AudioClip weaponSound;

    [Header("Sprite")]
    public Sprite weaponSprite;

    [Header("Weapon Settings")]
    public int bulletQuantity;
    public float weaponCadence = 0.05f;
    public float weaponCooldown = 1f;
    public float weaponDamage = 25f;
    public float weaponRecoil = 10000f;

    public void ApplyChanges(WeaponInfo changes)
    {
        bullet = changes.bullet != null ? changes.bullet : bullet;
        weaponSound = changes.weaponSound != null ? changes.weaponSound : weaponSound;

        bulletQuantity += changes.bulletQuantity;
        bulletSpeed += changes.bulletSpeed;
        bulletLife += changes.bulletLife;

        weaponCadence += changes.weaponCadence;
        weaponCooldown -= changes.weaponCooldown;
        weaponDamage += changes.weaponDamage;
        weaponRecoil += changes.weaponRecoil;
    }
}
