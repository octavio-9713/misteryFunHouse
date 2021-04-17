using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public GameObject weapon;
    protected HitAttack _hitAttack;

    private void Start()
    {
        _player = GameManager.Instance.player;
        _animator = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _rb = gameObject.GetComponent<Rigidbody2D>();

        _hitAttack = gameObject.GetComponentInChildren<HitAttack>();
        _hitAttack.enemy = this;
        _hitAttack.player = _player;

        currentState = new IdleMeleeState(this, _player, _animator);
    }

    protected override void Die()
    {
        base.Die();

        Destroy(weapon);
    }


    public override void Attack()
    {
        if (!attacking && !isDead)
        {
            Vector3 target = _player.transform.position - this.transform.position;
            transform.rotation = target.x < 0 ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);

            attacking = true;
            _animator.SetTrigger("shooting");

            fireEvent.Invoke();
            StartCoroutine(WaitForAttack());
        }
    }
}
