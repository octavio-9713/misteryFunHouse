using System;
using UnityEngine;

[Serializable]
public class WeaponInfo
{
    [Header ("Bullet Prefab")]
    public GameObject bullet;

    [Header("Weapon Sound")]
    public GameObject weaponSound;

    [Header("Weapon Sound")]
    public Sprite weaponSprite;

    [Header("Weapon Settings")]
    public int bulletQuantity;
    public float weaponRecoil = 10000f;
}
