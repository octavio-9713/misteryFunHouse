using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLifeEffect : ItemEffect
{
    public int lifeToIncrease;
    public bool recoversLife;

    public override void ApplyEffect()
    {
        Player player = GameManager.Instance.player;
        player.IncreaseLife(lifeToIncrease, recoversLife);
    }
}
