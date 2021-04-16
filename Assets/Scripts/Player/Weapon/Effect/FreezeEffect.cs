using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : TypeEffect
{
    [Header ("Freeze Settings")]
    public float speedSlowMultiplier = 0.75f;
    public float attackDelayMultiplier = 1.25f;
    public float attackSpeedtMultiplier = 0.75f;

    protected void OnDestroy()
    {
        DeFreezeEnemy();
        base.OnDestroy();
    }

    public override void ApplyEffect()
    {
        FreezeEnemy();
    }

    public void FreezeEnemy()
    {
        _enemy.stats.enemySpeed *= speedSlowMultiplier;
        _enemy.stats.attackDelay *= attackDelayMultiplier;
        _enemy.stats.bulletSpeed *= attackSpeedtMultiplier;
    }

    public void DeFreezeEnemy()
    {
        _enemy.stats.enemySpeed /= speedSlowMultiplier;
        _enemy.stats.attackDelay /= attackDelayMultiplier;
        _enemy.stats.bulletSpeed /= attackSpeedtMultiplier;

    }

}
