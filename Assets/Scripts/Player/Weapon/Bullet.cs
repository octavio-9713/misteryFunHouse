﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Rigidbody2D _rb;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float life = 1f;

    public Transform sight;
    protected Vector2 directionFromMouse;
    
    public Transform player;

    [Header("Bullet Effect")]
    public BulletEffect effect;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        sight = GameObject.FindGameObjectWithTag("Sight").transform;
        Destroy(gameObject, life);
        DetectSight();
    }

    
    void LateUpdate()
    {
        _rb.velocity = directionFromMouse * speed * Time.fixedDeltaTime;
    }


    public void DetectSight()
    {
        Vector2 sightPosition = sight.position;
        directionFromMouse = sightPosition - (Vector2)transform.position;
        directionFromMouse.Normalize();
    }

    protected void ApplyEffectToEnemy(Enemy enemy)
    {
        if (enemy.appliedEffects.Count > 0)
        {
            foreach (TypeEffect appliedEffect in enemy.appliedEffects)
            {
                if (appliedEffect.name.Contains(effect.effect.name))
                    Destroy(appliedEffect.gameObject);
            }
        }

        Instantiate(effect.effect, enemy.gameObject.transform);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.GetHit(damage, directionFromMouse);

            if (effect != null && effect.effect != null)
                ApplyEffectToEnemy(enemy);

            Destroy(gameObject);
        }

        else
        {
            if (col.gameObject.CompareTag("muro") || col.gameObject.CompareTag("Seek Missile"))
                Destroy(gameObject);
        }
    }
}
