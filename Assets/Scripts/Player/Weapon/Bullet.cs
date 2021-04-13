﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float life = 1f;
 
    protected Vector2 directionFromMouse;

    public Transform player;

    [Header("Bullet Effect")]
    public BulletEffect effect;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, life);
        DetectarMouse();
    }

    
    void LateUpdate()
    {
        _rb.velocity = directionFromMouse * speed * Time.fixedDeltaTime;
    }


    public void DetectarMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        directionFromMouse = mousePosition - (Vector2)transform.position;
        directionFromMouse.Normalize();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().GetHit(damage, directionFromMouse);

            if (effect != null && effect.effect != null)
                Instantiate(effect.effect, col.gameObject.transform);

            Destroy(gameObject);
        }

        else
        {
            if (col.gameObject.CompareTag("muro") || col.gameObject.CompareTag("Seek Missile"))
                Destroy(gameObject);
        }
    }
}
