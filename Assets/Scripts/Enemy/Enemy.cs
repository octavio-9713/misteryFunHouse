using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent deathEvent = new UnityEvent();

    public EnemyInfo stats;

    [Header("Attack Settings")]
    public GameObject[] shotpos;
    public bool attacking = false;

    protected Animator _animator;
    protected SpriteRenderer _renderer;
    protected ParticleSystem _ps;
    protected Rigidbody2D _rb;
    protected Player _player;

    [Header ("Audio Source")]
    public AudioSource audio;

    [Header("Wander Config")]
    public float wanderTimeDir = 1f;
    public float wanderRestTime = 3f;

    public State currentState;

    [HideInInspector]
    public bool needsWanderDirection = true;
    [HideInInspector]
    public Vector3 wanderTarget;

    protected bool _waitForHurt = false;

    private void Start()
    {
        _player = GameManager.Instance.player;
        _animator = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _ps = gameObject.GetComponentInChildren<ParticleSystem>();
        _rb = gameObject.GetComponent<Rigidbody2D>();

        currentState = new IdleState(this, _player, _animator);
    }

    private void Update()
    {
        currentState = currentState.Process();
    }

    public void MoveToTarget(Vector3 target, float speed)
    {
        if (!_waitForHurt)
        {
            _rb.AddForce(target * speed * Time.deltaTime);
            _animator.SetBool("move", target != Vector3.zero);
            transform.rotation = target.x < 0 ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);
        }
    }

    ////////////////// Status Methods //////////////////////
    
    public bool CanSeePlayer()
    {
        // Enemy layer distinta a Default para evitar el raycast (Mismo con Attack y Slash)
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            _player.transform.position - transform.position,
            stats.enemyVision,
            1 << LayerMask.NameToLayer("Default")
        );

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }

    public bool IsInPersonalSpace()
    {
        return Vector3.Distance(_player.transform.position, transform.position) < stats.personalSpace;
    }

    public bool IsWhitinAttackRange()
    {
        float dist = Vector3.Distance(_player.transform.position, transform.position);

        return dist > stats.attackRangeStart && dist < stats.attackRangeEnd;
    }

    ////////////////// Buff Methods //////////////////////

    public void BuffMeUp(BuffStats buff)
    {
        stats.ApplyBuff(buff);
        _ps.Play();
    }

    public void DeBuffMe(BuffStats buff)
    {
        stats.ReverseBuff(buff);
        _ps.Stop();
    }

    ////////////////// Attacking Methods //////////////////////

    public virtual void Attack()
    {
        if (!attacking && !_waitForHurt)
        {
            Vector3 target = _player.transform.position - this.transform.position;
            transform.rotation = target.x < 0 ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);

            _animator.SetTrigger("shooting");
            attacking = true;

            Shoot();
            StartCoroutine(WaitForAttack());
        }
    }

    protected virtual void Shoot()
    {
        foreach (GameObject pos in shotpos)
        {
            GameObject instance = Instantiate(stats.bullet, pos.transform.position, pos.transform.rotation);
            BulletEnemy bullet = instance.GetComponent<BulletEnemy>();

            bullet.speed = stats.bulletSpeed;
            bullet.damage = stats.enemyDamage;

            audio.PlayOneShot(stats.shootSound);
        }
    }

    ////////////////// Hurt Methods //////////////////////

    public virtual void GetHit(float value, Vector3 direction)
    {
        _waitForHurt = true;
        stats.enemyHealth -= value;

        _rb.AddForce(direction * 50000 * Time.deltaTime);

        if (stats.enemyHealth <= 0)
            _animator.SetTrigger("isDead");

        else
            _animator.SetTrigger("hurt");
    }

    public void FinishHurting()
    {
        this._waitForHurt = false;
        _animator.ResetTrigger("hurt");
    }


    public void Die()
    {
        deathEvent.Invoke();
        Destroy(gameObject);
    }

    ////////////////// Wander Methods //////////////////////
    
    public void WaitToWander()
    {
        StartCoroutine(WaitWanderDirection(wanderTimeDir));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "muro")
            wanderTarget *= -1;
    }

    protected IEnumerator WaitForAttack()
    {
        _animator.ResetTrigger("shooting");
        _animator.SetTrigger("waiting");
        yield return new WaitForSeconds(stats.attackDelay);
        attacking = false;
    }

    protected IEnumerator WaitWanderDirection(float time)
    {
        needsWanderDirection = false;
        yield return new WaitForSeconds(time);
        needsWanderDirection = true;
    }
}
