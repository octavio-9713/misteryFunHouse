using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : BulletEnemy
{
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameManager.Instance.player;

        //RotateTowardsPlayer();

        _dir = this.transform.right;

        Destroy(gameObject, 2f);

    }

    void LateUpdate()
    {
        _rb.velocity = _dir * speed * Time.fixedDeltaTime;
    }

    public void RotateTowardsPlayer()
    {
        Vector3 vectorToTarget = _player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
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
            Destroy(gameObject);
    }
}
