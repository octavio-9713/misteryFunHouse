using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : BulletEnemy
{
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameManager.Instance.player;

        _dir = this.transform.right;

        Destroy(gameObject, 2f);

    }

    void LateUpdate()
    {
        _rb.velocity = _dir * speed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Si chocamos contra el jugador o un bullet se borra
        if (col.gameObject.CompareTag("Player"))
        {
            if (!_player.dashing)
            {
                Vector3 damageDir = _player.transform.position - col.gameObject.transform.position;
                GameManager.Instance.player.GetHurt(damage, damageDir);
                Destroy(gameObject);
            }
        }
        else
        {
            if (col.gameObject.CompareTag("muro"))
                Destroy(gameObject);
        }
    }
}
