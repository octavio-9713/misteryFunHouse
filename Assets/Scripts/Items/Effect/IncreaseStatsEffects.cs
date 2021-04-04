using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStatsEffects : ItemEffect
{
    [Header ("Player Stats")]
    public PlayerStats stats;

    [Header("Dash Stats")]
    public float timeBetweenDashes;
    public float dashLength;
    public float dashWait;

    [Header ("Weapon Stats")]
    public WeaponInfo weapon;
    

    public override void ApplyEffect()
    {
        Player player = GameManager.Instance.player;
        player.ChangeStats(stats, timeBetweenDashes, dashLength, dashWait, weapon);
    }
}
