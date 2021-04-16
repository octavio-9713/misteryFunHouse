using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunGun : EnemyGun
{
    [Header ("Shotgun Bullets")]
    public int bulletQuantity = 4;

    public float dispertionH = 15f;
    public float dispertionY = 2;


    public override void Shoot(GameObject bullet, float speed, int damage, float nockback)
    {
        for (int i = 0; i < bulletQuantity; i++)
        {
            float dispercionH = Random.Range(-dispertionH, dispertionH);
            float dispercionHY = Random.Range(-dispertionY, dispertionY);

            Vector3 offset = new Vector3(dispercionH, dispercionHY, 0f);

            Vector3 spawnPos = shotpos.transform.position + offset;

            GameObject instance = Instantiate(bullet, spawnPos, shotpos.transform.rotation);
            BulletEnemy bInstance = instance.GetComponent<BulletEnemy>();

            bInstance.speed = speed;
            bInstance.damage = damage;
            bInstance.nockback = nockback;
        }
    }
}

