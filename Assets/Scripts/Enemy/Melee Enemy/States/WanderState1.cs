using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderMeleeState : WanderState
{
    protected float enterTime;

    public WanderMeleeState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
        name = STATE.WANDERING;
    }

    public override void Update()
    {
        Wander();

        float finalTime = Time.time - enterTime;
        if (_enemy.CanSeePlayer())
        {
            if (_enemy.IsWhitinAttackRange())
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
            if (Random.value <= 0.1f && finalTime > 10)
            {
                nextState = new IdleMeleeState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

        }
    }
}
