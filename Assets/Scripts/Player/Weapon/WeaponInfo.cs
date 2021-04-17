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

    public void ApplyChanges(WeaponBuff changes)
    {
        bullet = changes.bullet != null ? changes.bullet : bullet;
        weaponSound = changes.weaponSound != null ? changes.weaponSound : weaponSound;

        bulletQuantity = changes.bulletQuantity > 0 ? changes.bulletQuantity : 1;
        bulletSpeed *= changes.bulletSpeed > 0 ? changes.bulletSpeed : 1;
        bulletLife *= changes.bulletLife > 0 ? changes.bulletLife : 1;

        weaponCadence *= changes.weaponCadence > 0 ? changes.weaponCadence : 1;
        weaponCooldown /= changes.weaponCooldown > 0 ? changes.weaponCooldown : 1;
        weaponDamage *= changes.weaponDamage > 0 ? changes.weaponDamage : 1;
        weaponRecoil *= changes.weaponRecoil > 0 ? changes.weaponRecoil : 1;
    }

    public void UnApplyChanges(WeaponBuff changes)
    {
        bullet = changes.bullet != null ? changes.bullet : bullet;
        weaponSound = changes.weaponSound != null ? changes.weaponSound : weaponSound;

        bulletQuantity = 1;
        bulletSpeed /= changes.bulletSpeed > 0 ? changes.bulletSpeed : 1;
        bulletLife /= changes.bulletLife > 0 ? changes.bulletLife : 1;

        weaponCadence /= changes.weaponCadence > 0 ? changes.weaponCadence : 1;
        weaponCooldown *= changes.weaponCooldown > 0 ? changes.weaponCooldown : 1;
        weaponDamage /= changes.weaponDamage > 0 ? changes.weaponDamage : 1;
        weaponRecoil /= changes.weaponRecoil > 0 ? changes.weaponRecoil : 1;
    }
}
