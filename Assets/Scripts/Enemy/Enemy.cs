using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent deathEvent = new UnityEvent();

    public EnemyInfo stats;

    public bool attacking = false;

    protected Animator _animator;
    protected SpriteRenderer _renderer;
    protected Rigidbody2D _rg;
    protected Player _player;

    [Header("Wander Config")]
    public float wanderTimeDir = 1f;
    public float wanderRestTime = 3f;

    public State currentState;

    [HideInInspector]
    public bool needsWanderDirection = true;
    [HideInInspector]
    public Vector3 wanderTarget;


    private void Start()
    {
        _player = GameManager.Instance.player;
        _animator = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _rg = gameObject.GetComponent<Rigidbody2D>();

        currentState = new IdleState(this, _player, _animator);
    }

    private void Update()
    {
        currentState = currentState.Process();
    }

    public void MoveToTarget(Vector3 target, float speed)
    {
        _rg.AddForce(target * speed * Time.deltaTime);
        _animator.SetBool("move", target != Vector3.zero);
        _renderer.flipX = target.x < 0;
    }

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

    protected void Attack()
    {
        //poner la animacion de mover aca
        _animator.SetBool("move", false);

        ///-- Empezamos a atacar (importante una Layer en ataque para evitar Raycast)
        if (!attacking)
            StartCoroutine(WaitForAttack());
    }

    public void WaitToWander()
    {
        StartCoroutine(WaitWanderDirection(wanderTimeDir));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "muro")
            wanderTarget *= -1;

    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(stats.attackDelay);
        attacking = false;
    }

    IEnumerator WaitWanderDirection(float time)
    {
        needsWanderDirection = false;
        yield return new WaitForSeconds(time);
        needsWanderDirection = true;
    }

    IEnumerator Death(float seconds)
    {
        deathEvent.Invoke();
        //Instantiate(SonidoEnemy[0], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(seconds);
    }
}
