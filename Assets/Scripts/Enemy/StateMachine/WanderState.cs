using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    protected float enterTime;

    public WanderState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
        name = STATE.WANDERING;
    }

    public override void Enter()
    {
        _animator.SetBool("move", true);
        _animator.SetTrigger("move");
        enterTime = Time.time;
        base.Enter();
    }

    public override void Update()
    {
        Wander();

        float finalTime = Time.time - enterTime;
        if (_enemy.CanSeePlayer())
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

        else
        {
            if (Random.value <= 0.1f && finalTime > 10)
            {
                nextState = new IdleState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

        }
    }

    protected void Wander()
    {
        if (_enemy.needsWanderDirection)
        {
            _enemy.wanderTarget = GetWanderDirection();
            _enemy.WaitToWander();
        }

        _enemy.MoveToTarget(_enemy.wanderTarget, _enemy.stats.wanderSpeed);
    }

    private Vector3 GetWanderDirection()
    {
        Vector3 target = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), _enemy.transform.position.z);
        return target;
    }

    public override void Exit()
    {
        _animator.SetBool("move", false);
        _animator.ResetTrigger("move");
        base.Exit();
    }
}
