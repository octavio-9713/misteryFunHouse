using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEffect : TypeEffect
{
    [Header("Electric Settings")]
    public float electricDamage = 10f;

    protected void OnDestroy()
    {
        _enemy.fireEvent.RemoveListener(ElectrocuteEnemy);
        base.OnDestroy();
    }

    public override void ApplyEffect()
    {
        _enemy.fireEvent.AddListener(ElectrocuteEnemy);
    }

    public void ElectrocuteEnemy()
    {
        _enemy.GetHit(electricDamage, Vector3.zero);
    }
}
