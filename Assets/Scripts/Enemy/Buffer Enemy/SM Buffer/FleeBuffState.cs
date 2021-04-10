using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeBuffState : FleeState
{
    public FleeBuffState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator) { }

    public override void Update()
    {
        FleeFromPlayer();

        if (!_enemy.IsInPersonalSpace())
        {
            nextState = new IddleBuffState(_enemy, _player, _animator);
            _stage = EVENT.EXIT;
        }
    }

}
