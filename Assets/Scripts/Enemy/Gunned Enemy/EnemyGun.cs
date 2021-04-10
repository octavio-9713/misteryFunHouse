﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public Transform ContArma;
    public Transform shotpos;

    private Player _player;
    private Enemy _enemy;

    //sonido
    public AudioSource audioSource;
    public AudioClip gunSound;

    void Start()
    {
        _player = GameManager.Instance.player;
        _enemy = GetComponentInParent<Enemy>();

    }

    //para que gire el arma
    public void LateUpdate()
    {
        if (_enemy.currentState.name == State.STATE.ATTACK || _enemy.currentState.name == State.STATE.PERSUING)
            ContArma.up = ContArma.position - _player.transform.position;
    }


    public virtual void Shoot(GameObject bullet, float speed, int damage)
    {
        GameObject instance = Instantiate(bullet, shotpos.transform.position, Quaternion.identity);
        BulletEnemy bInstance = instance.GetComponent<BulletEnemy>();

        bInstance.speed = speed;
        bInstance.damage = damage;

        this.audioSource.PlayOneShot(gunSound);
    }
}
