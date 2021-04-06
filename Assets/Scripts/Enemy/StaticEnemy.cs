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
        _rg = gameObject.GetComponent<Rigidbody2D>();

        currentState = new IdleStateStatic(this, _player, _animator);
    }
}
