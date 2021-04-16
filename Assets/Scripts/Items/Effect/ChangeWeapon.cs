using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : ItemEffect
{
    [Header("Orbital Prefab")]
    public GameObject newGun;

    public override void ApplyEffect()
    {
        Player player = GameManager.Instance.player;
        player.ChangeWeapon(newGun);
    }
}
