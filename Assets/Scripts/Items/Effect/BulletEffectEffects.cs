using System;
using UnityEngine;

[Serializable]
public class BulletEffectEffects : ItemEffect
{
    [Header ("Player Stats")]
    public WeaponEffect effect;

    public override void ApplyEffect()
    {
        Player player = GameManager.Instance.player;
        player.ApplyWeaponEffect(effect);
    }

    public override void UnapplyEffect()
    {
        Player player = GameManager.Instance.player;
        player.ApplyWeaponEffect(effect);
    }
}
