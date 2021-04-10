using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        _animator.SetBool("move", false);
        base.Enter();
    }

    public override void Update()
    {
        _enemy.Attack();

        if (!_enemy.CanSeePlayer())
        {
            nextState = new WanderState(_enemy, _player, _animator);
            _stage = EVENT.EXIT;
        }

        else
        {
            if (!_enemy.IsWhitinAttackRange())
            {
                nextState = new PersueState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
