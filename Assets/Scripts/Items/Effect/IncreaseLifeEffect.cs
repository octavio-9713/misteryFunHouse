using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLifeEffect : ItemEffect
{
    [Header ("Life To Increase/Recover")]
    public int lifeToIncrease;

    [Header ("Settings")]
    public bool recoversLife;
    public bool justRecoversLife;

    public override void ApplyEffect()
    {
        Player player = GameManager.Instance.player;

        if (!justRecoversLife)
            player.IncreaseLife(lifeToIncrease, recoversLife);

        else
            player.RecoverLife(lifeToIncrease);
    }
}
