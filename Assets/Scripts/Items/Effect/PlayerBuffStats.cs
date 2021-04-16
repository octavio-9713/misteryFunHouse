using System;
using UnityEngine;

[Serializable]
public class PlayerBuffStats
{
    [Header("Player Settings")]
    public float playerSpeed = 1.5f;
    public float invencibilityTime = 1.5f;

    [Header("Dash Settings")]
    public float dashSpeed = 1.25f;
    public float initialDashForce = 1.15f;
    public float dashDamage = 1;

}
