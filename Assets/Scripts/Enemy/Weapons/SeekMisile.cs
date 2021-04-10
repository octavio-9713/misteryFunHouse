using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekMisile : BulletEnemy
{

    [Header("Explode Time")]
    public float explodeTime;

    [Header("Explode Time")]
    public float enemyExplodeDamage;
    public BulletExplotion Explotion;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameManager.Instance.player;

        Invoke("Explode", explodeTime);
    }


    void LateUpdate()
    {
        Vector3 dir = _player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed / 2);

        _dir = this.transform.right;
        _rb.velocity = _dir * speed * Time.fixedDeltaTime;
    }

    public void Explode()
    {
        BulletExplotion exploded = Instantiate(Explotion, transform.position, transform.rotation);
        exploded.damageToEnemy = enemyExplodeDamage;
        exploded.damageToPlayer = damage;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Si chocamos contra el jugador o un bullet se borra
        if (col.gameObject.CompareTag("Player"))
        {
            if (!_player.dashing)
            {
                Explode();
            }
        }
        else
        {
            Explode();
        }
    }
}
