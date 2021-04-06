using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{
    public FleeState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
        name = STATE.FLEEING;
    }

    public override void Enter()
    {
        _animator.SetBool("move", true);
        base.Enter();
    }

    public override void Update()
    {
        FleeFromPlayer();

        if (_enemy.IsWhitinAttackRange())
        {
            nextState = new AttackState(_enemy, _player, _animator);
            _stage = EVENT.EXIT;
        }
    }

    protected void FleeFromPlayer()
    {
        FleeFromDirection(_player.transform.position);
    }


    protected void FleeFromDirection(Vector3 direction)
    {
        Vector3 fleeDirection = _enemy.transform.position - direction;
        _enemy.MoveToTarget(fleeDirection.normalized, _enemy.stats.enemySpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
