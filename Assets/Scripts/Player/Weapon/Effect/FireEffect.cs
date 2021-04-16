using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : TypeEffect
{
    [Header("Fire Settings")]
    public float fireDamage = 10f;
    public float timeToApply = 1f;
    
    public override void ApplyEffect()
    {
        _enemy.GetHit(fireDamage, Vector3.zero);
        Invoke(nameof(ApplyEffect), timeToApply);
    }

}
