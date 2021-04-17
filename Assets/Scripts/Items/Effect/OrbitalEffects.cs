using System;
using UnityEngine;

[Serializable]
public class OrbitalEffects : ItemEffect
{
    [Header ("Orbital Prefab")]
    public Orbital orbitalPrefab;

    public override void ApplyEffect()
    {
        Player player = GameManager.Instance.player;
        Orbital orbital = Instantiate(orbitalPrefab, player.transform);
        orbital.playerSight = player.sight;
    }

    public override void UnapplyEffect()
    {
        Player player = GameManager.Instance.player;
        Orbital orbital = player.GetComponentInChildren<Orbital>();
        Destroy(orbital.gameObject);
    }


}
