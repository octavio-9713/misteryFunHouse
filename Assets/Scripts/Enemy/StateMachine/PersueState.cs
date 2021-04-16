using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersueState : State
{
    public PersueState(Enemy enemy, Player player, Animator animator) : base(enemy, player, animator)
    {
        name = STATE.PERSUING;
    }

    public override void Enter()
    {
        _animator.SetBool("move", true);
        base.Enter();
    }

    public override void Update()
    {
        PersuePlayer();
        if (!_enemy.CanSeePlayer())
        {
            nextState = new WanderState(_enemy, _player, _animator);
            _stage = EVENT.EXIT;
        }

        else
        {
            if (_enemy.IsWhitinAttackRange())
            {
                nextState = new AttackState(_enemy, _player, _animator);
                _stage = EVENT.EXIT;
            }

        }
    }
    protected void PersuePlayer()
    {
        Vector3 targetDir = _player.transform.position - _enemy.transform.position;

        if (!_player.moving)
            FollowDirection(targetDir);

        else
        {
            float lookAhead = targetDir.magnitude / _player.stats.playerSpeed;
            Vector3 playerHeading = _player.MovingDirection();

            FollowDirection(targetDir + playerHeading * lookAhead);
        }
    }

    protected void FollowDirection(Vector3 direction)
    {
        _enemy.MoveToTarget(direction.normalized, _enemy.stats.enemySpeed);
    }

    public override void Exit()
    {
        _animator.SetBool("move", false);
        base.Exit();
    }
}
