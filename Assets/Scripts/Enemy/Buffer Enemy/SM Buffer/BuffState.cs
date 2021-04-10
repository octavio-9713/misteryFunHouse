using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffState : AttackState
{
    public BuffState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator) { }

    public override void Update()
    {
        if (!_enemy.attacking)
        {
            if (!_enemy.CanSeePlayer())
            {
                nextState = new WanderState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
                return ;
            }

            else
            {
                if (_enemy.IsInPersonalSpace())
                {
                    nextState = new FleeBuffState(_enemy, _player, _animator);
                    _stage = EVENT.EXIT;
                    return;
                }
            }

            _enemy.Attack();
        }
    }


}
