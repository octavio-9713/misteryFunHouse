using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferEnemy : Enemy
{
    public BuffStats buff;

    public bool waitToBuff;

    [Header("Buffed Halo")]
    public GameObject bufferHalo;

    [Header("Posible Buffed Enemies")]
    public List<GameObject> allies;
    private Enemy _buffedEnemy;

    private void Start()
    {
        _player = GameManager.Instance.player;
        _animator = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _rb = gameObject.GetComponent<Rigidbody2D>();

        currentState = new IddleBuffState(this, _player, _animator);

        allies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        allies = allies.FindAll(allie => allie.GetComponent<BufferEnemy>() == null);
    }

    public override void Attack()
    {
        if (!attacking && !_waitForHurt)
        {
            _animator.SetBool("attacking", true);
            attacking = true;

            Buff();

            fireEvent.Invoke();
            bufferHalo.SetActive(true);
        }
    }

    private void Buff()
    {
        GameObject choosenOne = allies[Random.Range(0, allies.Count)];
        _buffedEnemy = choosenOne.GetComponent<Enemy>();
        _buffedEnemy.BuffMeUp(buff);
        _buffedEnemy.deathEvent.AddListener(DeathOfChoosenOne);
    }

    private void DeathOfChoosenOne()
    {
        attacking = false;
        bufferHalo.SetActive(false);
        StartCoroutine(WaitForBuff());
    }

    private void DebuffChosen()
    {
        _buffedEnemy.DeBuffMe(buff);
    }

    public override void GetHit(float value, Vector3 direction)
    {
        if (!_waitForHurt)
        {
            _waitForHurt = true;
            stats.enemyHealth -= value;

            _rb.AddForce(direction * 50000 * Time.deltaTime);

            if (stats.enemyHealth <= 0)
                Die();

            else
                _animator.SetTrigger("hurt");

            attacking = false;
            bufferHalo.SetActive(false);

            StartCoroutine(WaitForBuff());
            DebuffChosen();
        }
    }

    protected IEnumerator WaitForBuff()
    {
        _animator.SetBool("attacking", false);
        yield return new WaitForSeconds(stats.attackDelay);
        waitToBuff = false;
    }
}
