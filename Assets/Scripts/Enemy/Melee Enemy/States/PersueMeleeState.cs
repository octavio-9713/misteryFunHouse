using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersueMeleeState : PersueState
{
    public PersueMeleeState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
        name = STATE.PERSUING;
    }

    public override void Update()
    {
        PersuePlayer();
        if (!_enemy.CanSeePlayer())
        {
            nextState = new WanderMeleeState(_enemy, _player, _animator);
            _stage = EVENT.EXIT;
        }

        else
        {
            if (_enemy.IsWhitinAttackRange())
            {
                nextState = new AttackMeleeState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

        }
    }

}
