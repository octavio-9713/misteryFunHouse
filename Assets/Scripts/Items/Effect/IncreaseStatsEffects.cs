using System;
using UnityEngine;

[Serializable]
public class IncreaseStatsEffects : ItemEffect
{
    [Header ("Player Stats")]
    public PlayerBuffStats buffstats;

    [Header("Dash Stats")]
    public float timeBetweenDashes;
    public float dashLength;
    public float dashCooldown;

    [Header ("Weapon Stats")]
    public WeaponBuff buff;
    

    public override void ApplyEffect()
    {
        Player player = GameManager.Instance.player;
        player.ChangeStats(buffstats, timeBetweenDashes, dashLength, dashCooldown, buff);
    }
}
