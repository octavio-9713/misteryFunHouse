using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy
{
    private void Start()
    {
        _player = GameManager.Instance.player;
        _animator = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _rb = gameObject.GetComponent<Rigidbody2D>();

        currentState = new IdleStateStatic(this, _player, _animator);
    }

    public override void GetHit(float value, Vector3 direction)
    {
        _waitForHurt = true;
        stats.enemyHealth -= value;

        if (stats.enemyHealth <= 0)
            _animator.SetTrigger("isDead");

        else
            _animator.SetTrigger("hurt");
    }
}
