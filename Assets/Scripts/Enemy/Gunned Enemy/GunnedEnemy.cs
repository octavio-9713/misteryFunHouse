using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnedEnemy : Enemy
{

    public EnemyGun gun;

    public GameObject weaponContainer;

    protected override void Shoot()
    {
        _animator.SetTrigger("shooting");
        gun.Shoot(stats.bullet, stats.bulletSpeed, stats.enemyDamage, stats.bulletNockback);
    }

    public override void GetHit(float value, Vector3 direction)
    {
        if (!_waitForHurt)
        {
            _waitForHurt = true;
            stats.enemyHealth -= value;

            _rb.AddForce(direction * 50000 * Time.deltaTime);

            if (stats.enemyHealth <= 0)
            {
                Die();
                weaponContainer.SetActive(false);
            }

            else
                _animator.SetTrigger("hurt");
        }
    }
}
