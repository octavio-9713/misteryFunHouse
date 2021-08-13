using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [Header("Explotion")]
    public BulletExplotion explotionPrefab;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(Explode), life);
        DetectSight();
    }


    public void Explode()
    {
        BulletExplotion exploded = Instantiate(explotionPrefab, transform.position, transform.rotation);
        exploded.damageToEnemy = damage;
        exploded.canDamageEnemy = true;
        exploded.effect = effect;

        if (effect)
            Instantiate(effect, exploded.gameObject.transform);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {

            Explode();
            Destroy(gameObject);
        }

        else
        {
            if (col.gameObject.CompareTag("muro") || col.gameObject.CompareTag("Seek Missile"))
            {
                Explode();
                Destroy(gameObject);
            }
        }
    }
}
