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
        Instantiate(orbitalPrefab, player.transform);
    }
}
