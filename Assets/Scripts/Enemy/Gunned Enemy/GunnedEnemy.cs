using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnedEnemy : Enemy
{

    public EnemyGun gun;

    public GameObject weaponContainer;

    protected override void Shoot()
    {
        gun.Shoot(stats.bullet, stats.bulletSpeed, stats.enemyDamage);
    }

    public override void GetHit(float value, Vector3 direction)
    {
        _waitForHurt = true;
        stats.enemyHealth -= value;

        _rb.AddForce(direction * 50000 * Time.deltaTime);

        if (stats.enemyHealth <= 0)
        {
            weaponContainer.SetActive(false);
            _animator.SetTrigger("isDead");
        }

        else
            _animator.SetTrigger("hurt");
    }
}
