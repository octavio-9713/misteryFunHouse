using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected float enterTime;

    public IdleState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
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
        if (_enemy.CanSeePlayer())
        {
            if (_enemy.IsInPersonalSpace())
            {
                nextState = new FleeState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

            else
            {
                if (_enemy.IsWhitinAttackRange())
                {
                    nextState = new AttackState(_enemy, _player, _animator);
                    _stage = EVENT.EXIT;
                }
                else
                {
                    nextState = new PersueState(_enemy, _player, _animator);
                    _stage = EVENT.EXIT;
                }
            }
        }

        else
        {
            if (Random.value <= 0.1f && finalTime >= 3)
            {
                nextState = new WanderState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
