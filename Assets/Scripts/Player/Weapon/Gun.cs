﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Weapon Shot Position")]
    public Transform shotpos;

    [Header("Weapon Settings")]
    public WeaponInfo weapon = new WeaponInfo();

    [Header("Weapon Effect")]
    public List<WeaponEffect> effects = new List<WeaponEffect>();
    
    [Header("Sight and Containers")]
    public Transform sight;
    public Transform container;

    [Header("Audio")]
    public AudioSource audioSource;

    private Player _player;
    private SpriteRenderer _renderer;
    private bool _canShoot = true;

    void Start()
    {
        _player = GameManager.Instance.player;
        _renderer = GetComponent<SpriteRenderer>();
        Debug.Log(weapon.weaponSprite);
        GameManager.Instance.gameUi.ChangePanel(weapon.weaponSprite);
    }


    void Update()
    {
        DetectMouse();

        if (Input.GetButton("Fire1") && CanShoot())
            Shoot();

        _renderer.flipY = sight.transform.position.x < transform.position.x;
    }

    public void LateUpdate()
    {
        container.up = container.position - sight.position;
    }

    /////////////////// Shoot Methods //////////////////////////

    private bool CanShoot()
    {
        return _canShoot && !_player.dashing;
    }

    public void Shoot()
    {
        StartCoroutine(InstantiateShoot());
        StartCoroutine(WaitToShoot(weapon.weaponCooldown));
    }

    public void DetectMouse()
    {
        sight.position = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
            ));
    }


    /////////////////// Shoot Methods //////////////////////////
    
    protected bool SpawnBulletWithEffect()
    {
        if (effects == null || effects.Count == 0)
            return false;

        float range = Random.Range(0, 100);
        return range <= EffectProbability();
    }

    protected float EffectProbability()
    {
        float prob = 0;
        foreach (WeaponEffect effect in effects)
            prob += effect.probability;

        return prob;
    }

    public void ApplyEffectToBullet(Bullet bullet)
    {
        WeaponEffect effect = effects[Random.Range(0, effects.Count)];
        bullet.speed *= effect.bulletSpeedMultiplier;
        bullet.damage *= effect.bulletDamageMultiplier;
        bullet.life *= effect.bulletLifeMultiplier;
        bullet.effect = effect.effect;

        Instantiate(effect.effect, bullet.transform);
    }

    /////////////////// Apply Methods //////////////////////////

    public void ApplyChanges(WeaponInfo weaponInfo)
    {
        this.weapon.ApplyChanges(weaponInfo);
    }

    public void ApplyEffect(WeaponEffect effect)
    {
        effects.Add(effect);
    }

    /////////////////// Enumerators //////////////////////////
    ///
    IEnumerator WaitToShoot(float seconds)
    {
        _canShoot = false;
        yield return new WaitForSeconds(seconds);
        _canShoot = true;
    }

    IEnumerator InstantiateShoot()
    {
        for (int i = 0; i < weapon.bulletQuantity; i++)
        {
            GameObject instance = Instantiate(weapon.bullet, shotpos.transform.position, Quaternion.identity);
            Bullet bullet = instance.GetComponent<Bullet>();

            bullet.speed = weapon.bulletSpeed;
            bullet.damage = weapon.weaponDamage;
            bullet.life = weapon.bulletLife;

            AudioClip shootSound = weapon.weaponSound;

            if (SpawnBulletWithEffect())
            {
                Debug.Log("Applying Effect");
                ApplyEffectToBullet(bullet);

                if (weapon.weaponSound)
                    shootSound = weapon.weaponSound;
            }

            audioSource.PlayOneShot(shootSound);
            yield return new WaitForSeconds(weapon.weaponCadence);
        }
    }
}