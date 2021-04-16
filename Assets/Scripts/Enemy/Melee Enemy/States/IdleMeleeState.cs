using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMeleeState : IdleState
{
    protected float enterTime;

    public IdleMeleeState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
        name = STATE.IDLE;
    }

    public override void Update()
    {
        float finalTime = Time.time - enterTime;
        if (_enemy.CanSeePlayer())
        {
            if(_enemy.IsWhitinAttackRange())
            {
                nextState = new AttackMeleeState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

            else
            {
                nextState = new PersueMeleeState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }
        }

        else
        {
            if (Random.value <= 0.1f && finalTime >= 3)
            {
                nextState = new WanderMeleeState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

        }
    }
}
