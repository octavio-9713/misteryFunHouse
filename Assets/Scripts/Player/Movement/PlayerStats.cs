using System;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    [Header("Player Life")]
    public int maxHp = 3;

    [HideInInspector]
    public int currentHp;

    [Header("Player Settings")]
    public float playerSpeed = 10000;
    public float invencibilityTime = 0.5f;

    [Header ("Dash Settings")]
    public float dashSpeed;
    public float initialDashForce = 8000;
    public float dashDamage;


    public void ApplyStats(PlayerStats stats)
    {
        this.maxHp += stats.maxHp;

        playerSpeed += stats.playerSpeed;

        dashSpeed += stats.dashSpeed;
        initialDashForce += stats.initialDashForce;

        invencibilityTime += stats.invencibilityTime;
    }

    public void ApplyStats(PlayerBuffStats stats)
    {
        playerSpeed *= stats.playerSpeed > 0 ? stats.playerSpeed : 1;
        invencibilityTime *= stats.invencibilityTime > 0 ? stats.invencibilityTime : 1;

        dashSpeed *= stats.dashSpeed > 0 ? stats.dashSpeed : 1;
        initialDashForce *= stats.initialDashForce > 0 ? stats.initialDashForce : 1;
        dashDamage *= stats.dashDamage > 0 ? stats.dashDamage : 1;
    }

}
