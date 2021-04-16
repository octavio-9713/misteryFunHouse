using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotgun : Gun
{
    [Header("Dispertion Settings")]
    public float dispertionH = 15f;
    public float dispertionY = 3f;

    public override void Shoot()
    {
        for (int i = 0; i < weapon.bulletQuantity; i++)
        {
            float dispercionH = Random.Range(-dispertionH, dispertionH);
            float dispercionHY = Random.Range(-dispertionY, dispertionY);

            Vector3 offset = new Vector3(dispercionH, dispercionHY, 0f);

            Vector3 spawnPos = shotpos.transform.position + offset;

            GameObject instance = Instantiate(weapon.bullet, spawnPos, shotpos.transform.rotation);
            Bullet bullet = instance.GetComponent<Bullet>();

            bullet.speed = weapon.bulletSpeed;
            bullet.damage = weapon.weaponDamage;
            bullet.life = weapon.bulletLife;

            AudioClip shootSound = weapon.weaponSound;

            if (SpawnBulletWithEffect())
            {
                ApplyEffectToBullet(bullet);

                if (weapon.weaponSound)
                    shootSound = weapon.weaponSound;
            }

            audioSource.PlayOneShot(shootSound);

        }

        _player.Recoil(weapon.weaponRecoil);
        StartCoroutine(WaitToShoot(weapon.weaponCooldown));
    }
}