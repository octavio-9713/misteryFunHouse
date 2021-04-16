using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplotion : MonoBehaviour
{
    [HideInInspector]
    public float damageToEnemy;
    [HideInInspector]
    public int damageToPlayer;

    public BulletEffect effect;
    public bool canDamageEnemy = false;

    public void EndExplotion()
    {
        Destroy(gameObject);
    }

    private void ApplyEffectToEnemy(Enemy enemy)
    {
        if (enemy.appliedEffects.Count > 0)
        {
            foreach (TypeEffect appliedEffect in enemy.appliedEffects)
            {
                if (appliedEffect.name.Contains(effect.effect.name))
                    Destroy(appliedEffect.gameObject);
            }
        }

        Instantiate(effect.effect, enemy.gameObject.transform);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Enemy") && canDamageEnemy)
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            Vector3 damageDir = enemy.transform.position - col.gameObject.transform.position;
            col.gameObject.GetComponent<Enemy>().GetHit(damageToEnemy, damageDir);

            if (effect != null && effect.effect != null)
                ApplyEffectToEnemy(enemy);
        }

        if (col.gameObject.CompareTag("Player") && !canDamageEnemy)
        {
            Player player = col.gameObject.GetComponent<Player>();
            if (!player.dashing)
            {
                Vector3 damageDir = player.transform.position - col.gameObject.transform.position;
                GameManager.Instance.player.GetHurt(damageToPlayer, damageDir, 4000);
            }
        }
    }
}
