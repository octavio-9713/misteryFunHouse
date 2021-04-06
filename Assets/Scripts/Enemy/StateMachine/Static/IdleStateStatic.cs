using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateStatic : State
{
    private float enterTime;

    public IdleStateStatic(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        _animator.SetBool("move", false);
        enterTime = Time.time;
        base.Enter();
    }

    public override void Update()
    {
        float finalTime = Time.time - enterTime;
        if (_enemy.IsWhitinAttackRange())
        {
            nextState = new AttackStateStatic(_enemy, _player, _animator);
            _stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
