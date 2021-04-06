using System;
using UnityEngine;

[Serializable]
public class WeaponInfo
{
    [Header ("Bullet Prefab")]
    public GameObject bullet;

    [Header("Weapon Sound")]
    public GameObject weaponSound;

    [Header("Sprite")]
    public Sprite weaponSprite;

    [Header("Weapon Settings")]
    public int bulletQuantity;
    public float weaponCadence = 0.05f;
    public float weaponDamage = 25f;
    public float weaponRecoil = 10000f;

    public void ApplyChanges(WeaponInfo changes)
    {
        bullet = changes.bullet != null ? changes.bullet : bullet;
        weaponSound = changes.weaponSound != null ? changes.weaponSound : weaponSound;

        bulletQuantity = changes.bulletQuantity;
        weaponRecoil = changes.weaponRecoil;
    }
}
