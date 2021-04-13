using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public int damage;

    protected Player _player;

    protected Rigidbody2D _rb;
    protected Vector3 _dir;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameManager.Instance.player;

        _dir = (_player.transform.position - this.transform.position).normalized;

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

    //public void ShotGunDisparo()
    //{
    //    float dispercionH = Random.Range(-1f, 1f);
    //    float dispercionHY = Random.Range(-1f, 1f);

    //    Vector3 offset = new Vector3(dispercionH, dispercionHY, 0f);

    //    direction = dir + offset;

    //    rb2d.velocity = direction * speed * Time.fixedDeltaTime;

    //}

}
