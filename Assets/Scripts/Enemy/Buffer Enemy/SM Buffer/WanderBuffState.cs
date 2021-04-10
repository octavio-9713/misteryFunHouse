using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBuffState : WanderState
{
    public WanderBuffState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
    }

    public override void Update()
    {
        Wander();

        float finalTime = Time.time - enterTime;
        if (_enemy.CanSeePlayer())
        {
            if (_enemy.IsInPersonalSpace())
            {
                nextState = new FleeBuffState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

            else
            {
                nextState = new BuffState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
                
            }
        }

        else
        {
            if (Random.value <= 0.1f && finalTime > 10)
            {
                nextState = new IddleBuffState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

        }
    }
}
