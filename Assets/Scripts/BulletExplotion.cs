using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplotion : MonoBehaviour
{
    [HideInInspector]
    public float damageToEnemy;
    [HideInInspector]
    public int damageToPlayer;

    public void EndExplotion()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            Vector3 damageDir = enemy.transform.position - col.gameObject.transform.position;
            col.gameObject.GetComponent<Enemy>().GetHit(damageToEnemy, damageDir);
        }

        if (col.gameObject.CompareTag("Player"))
        {
            Player player = col.gameObject.GetComponent<Player>();
            if (!player.dashing)
            {
                Vector3 damageDir = player.transform.position - col.gameObject.transform.position;
                GameManager.Instance.player.GetHurt(damageToPlayer, damageDir);
            }
        }
    }
}
