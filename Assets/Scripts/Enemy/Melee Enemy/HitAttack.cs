using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAttack : MonoBehaviour
{
    
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Enemy enemy;

    void OnTriggerEnter2D(Collider2D col)
    {
        // Si chocamos contra el jugador o un bullet se borra
        if (col.gameObject.CompareTag("Player"))
        {
            if (!player.dashing)
            {
                Vector3 damageDir = player.transform.position - enemy.transform.position;
                GameManager.Instance.player.GetHurt(enemy.stats.enemyDamage, damageDir, enemy.stats.bulletNockback);
            }
        }
    }
}
