using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent deathEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent fireEvent = new UnityEvent();

    protected bool isDead = false;
    protected bool isAsleep = false;

    public EnemyInfo stats;

    [Header("Attack Settings")]
    public GameObject[] shotpos;
    public bool attacking = false;


    [Header("Buff")]
    public GameObject buffHalo;

    protected Animator _animator;
    protected SpriteRenderer _renderer;

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

    [HideInInspector]
    public List<TypeEffect> appliedEffects = new List<TypeEffect>();

    private void Start()
    {
        _player = GameManager.Instance.player;
        _animator = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _rb = gameObject.GetComponent<Rigidbody2D>();

        currentState = new IdleState(this, _player, _animator);
    }

    private void Update()
    {
        if (!isAsleep)
            currentState = currentState.Process();

        if (CanSeePlayer())
            transform.rotation = _player.transform.position.x < transform.position.x ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);
    }

    public void MoveToTarget(Vector3 target, float speed)
    {
        if (!isDead)
        {
            _rb.AddForce(target * speed * Time.deltaTime);
            _animator.SetBool("move", target != Vector3.zero);
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
            if (hit.collider.CompareTag("Player") && !_player.death)
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

        return dist > stats.attackRangeStart && dist < stats.attackRangeEnd && !_player.death;
    }

    ////////////////// Buff Methods //////////////////////

    public void BuffMeUp(BuffStats buff)
    {
        stats.ApplyBuff(buff);
        buffHalo.SetActive(true);
    }

    public void DeBuffMe(BuffStats buff)
    {
        stats.ReverseBuff(buff);
        buffHalo.SetActive(false);
    }

    ////////////////// Attacking Methods //////////////////////

    public virtual void Attack()
    {
        if (!attacking && !isDead)
        {
            Vector3 target = _player.transform.position - this.transform.position;
            transform.rotation = target.x < 0 ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);

            _animator.SetTrigger("shooting");
            attacking = true;

            Shoot();

            fireEvent.Invoke();
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
            bullet.nockback = stats.bulletNockback;
            bullet.damage = stats.enemyDamage;

            audio.PlayOneShot(stats.shootSound);
        }
    }

    ////////////////// Hurt Methods //////////////////////

    public virtual void GetHit(float value, Vector3 direction)
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
        }
    }

    public void FinishHurting()
    {
        this._waitForHurt = false;
        _animator.ResetTrigger("hurt");
    }

    protected virtual void Die()
    {
        isDead = true;
        _animator.SetTrigger("isDead");

        transform.rotation = Quaternion.Euler(0, 0, 0);
        _renderer.flipX = false;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = 0;
    }


    public void FinishDeath()
    {
        deathEvent.Invoke();
        Destroy(gameObject);
    }

    ////////////////// Other Methods //////////////////////
    
    public void WaitToWander()
    {
        StartCoroutine(WaitWanderDirection(wanderTimeDir));
    }

    public void WaitToWake(float sleepTime)
    {
        StartCoroutine(WakeUp(sleepTime));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "muro")
            wanderTarget *= -1;
    }

    protected IEnumerator WakeUp(float timeTo)
    {
        isAsleep = true;
        yield return new WaitForSeconds(timeTo);
        isAsleep = false;
    }

    protected IEnumerator WaitForAttack()
    {
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
