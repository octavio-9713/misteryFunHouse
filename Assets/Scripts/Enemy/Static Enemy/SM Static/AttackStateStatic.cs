using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateStatic : State
{
    public AttackStateStatic(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        _animator.SetBool("move", false);
        _animator.SetTrigger("shooting");
        base.Enter();
    }

    public override void Update()
    {
        _enemy.Attack();

        if (!_enemy.attacking)
        {
            if (!_enemy.IsWhitinAttackRange())
            {
                    nextState = new IdleStateStatic(_enemy, _player, _animator);
                    _stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        _animator.ResetTrigger("shooting");
        base.Exit();
    }
}
