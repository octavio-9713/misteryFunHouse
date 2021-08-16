using System;
using UnityEngine;

[Serializable]
public class OrbitalEffects : ItemEffect
{
    [Header ("Orbital Prefab")]
    public GameObject orbitalPrefab;

    private GameObject spawnedOrbital;

    public override void ApplyEffect()
    {
        Player player = GameManager.Instance.player;
        GameObject spawnedOrbital = Instantiate(orbitalPrefab);
        Orbital orbital = spawnedOrbital.GetComponentInChildren<Orbital>();
        orbital.playerSight = player.sight;
    }

    public override void UnapplyEffect()
    {
        Destroy(spawnedOrbital);
    }


}
