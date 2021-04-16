using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public Transform ContArma;
    public Transform shotpos;

    private Player _player;
    private Enemy _enemy;
    private SpriteRenderer _renderer;

    //sonido
    public AudioSource audioSource;
    public AudioClip gunSound;

    void Start()
    {
        _player = GameManager.Instance.player;
        _enemy = GetComponentInParent<Enemy>();
        _renderer = GetComponentInParent<SpriteRenderer>();

    }

    //para que gire el arma
    public void LateUpdate()
    {
        if (_enemy.currentState.name == State.STATE.ATTACK || _enemy.currentState.name == State.STATE.PERSUING)
            ContArma.up = ContArma.position - _player.transform.position;

        _renderer.flipY = _player.transform.position.x < 0;
    }


    public virtual void Shoot(GameObject bullet, float speed, int damage, float nockback)
    {
        GameObject instance = Instantiate(bullet, shotpos.transform.position, Quaternion.identity);
        BulletEnemy bInstance = instance.GetComponent<BulletEnemy>();

        bInstance.speed = speed;
        bInstance.damage = damage;
        bInstance.nockback = nockback;

        this.audioSource.PlayOneShot(gunSound);
    }
}
