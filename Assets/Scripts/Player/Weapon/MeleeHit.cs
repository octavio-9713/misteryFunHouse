using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : Bullet
{
    public GameObject defaultBullet;

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Si chocamos contra el jugador o un bullet se borra
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.GetHit(damage, directionFromMouse);

            if (effect != null && effect.effect != null)
                ApplyEffectToEnemy(enemy);

        }

        else
        {
            if (col.gameObject.CompareTag("BulletsEnemy"))
            {
                ContactPoint2D[] contacts = new ContactPoint2D[5];

                Vector3 returnPos = (Vector2) gameObject.transform.position - contacts[0].point;

                GameObject instBullet = Instantiate(defaultBullet, returnPos, Quaternion.identity);
                Bullet bullet = instBullet.GetComponent<Bullet>();
                bullet.damage = damage / 2;
                bullet.life = life;

                Destroy(col.gameObject);
            }
        }
    }
}
