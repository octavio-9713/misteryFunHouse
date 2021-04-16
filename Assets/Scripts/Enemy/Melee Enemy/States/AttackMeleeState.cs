using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMeleeState : AttackState
{
    public AttackMeleeState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
        name = STATE.ATTACK;
    }

    public override void Update()
    {
        if (!_enemy.attacking)
        {
            if (!_enemy.CanSeePlayer())
            {
                nextState = new WanderMeleeState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

            else
            {
                if (!_enemy.IsWhitinAttackRange())
                {
                    nextState = new PersueMeleeState(_enemy, _player, _animator);
                    _stage = EVENT.EXIT;
                }
                else
                    _enemy.Attack();
            }
        }
    }
}
