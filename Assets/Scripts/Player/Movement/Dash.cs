using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashLengthSec;

    [Header("Dash Wait Time")]
    public float dashWait = 0.35f;
    public float dashCooldown = 2.5f;

    private Player _player;
    private Move _move;
    private Rigidbody2D _rb;
    private Animator _animator;

    private bool _dashing = false;

    private float _dashTimeCounter;

    private bool _dashEnabled = true;

    private Vector2 _dashDirection;
    private float _dashStartTime;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = this.GetComponent<Player>();
        _move = this.GetComponent<Move>();
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //el uso del dash
        if (Input.GetMouseButtonDown(1) && _dashEnabled)
            this.DoADash();

        else
        {
            this.CheckTime();
        }

    }

    void FixedUpdate()
    {
        if (_dashing)
        {
            _rb.velocity = _dashDirection * _player.stats.dashSpeed * Time.fixedDeltaTime;
        }

    }

    //Cambiar hacia adonde te moves...
    private void DoADash()
    {
        float horMov = Input.GetAxisRaw("Horizontal");
        float verMov = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(horMov, verMov);
        if (move != Vector2.zero)
        {
            _move.DisableMove();
        }

        if (_dashEnabled)
        {
            PrepareDash(move);

            _animator.SetBool("dash", true);
            _player.PlayDashSound();

            StartCoroutine(DashWait(dashCooldown));
        }
    }

    private void CheckTime()
    {
        if (_dashing)
        {
            if (_dashTimeCounter >= dashLengthSec)
            {
                StopDash();
            }
            else
            {
                _dashTimeCounter = Time.time - _dashStartTime;
            }
        }
    }

    public void StopDash()
    {
        this._dashing = false;
        _player.dashing = false;
        _dashTimeCounter = dashLengthSec;
        _animator.SetBool("dash", false);

        _move.EnableMove();
        _player.DashReadySound();

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = 0;
    }

    void PrepareDash(Vector2 dashDirection)
    {
        _dashing = true;
        _player.dashing = true;

        if (dashDirection == Vector2.zero)
        {
            _dashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _dashDirection -= (Vector2)transform.position;
        }

        else
            _dashDirection = dashDirection;

        _dashDirection.Normalize();
        _player.PlayDashSound();
        _dashStartTime = Time.time;
        _dashTimeCounter = 0;
    }
    public void ChangeStats(float timeBetweenDashes, float dashLength, float dashWait)
    {
        this.dashWait -= timeBetweenDashes;
        this.dashLengthSec += dashLength;
        this.dashCooldown -= dashWait;
    }

    public void UnChangeStats(float timeBetweenDashes, float dashLength, float dashWait)
    {
        this.dashWait += timeBetweenDashes;
        this.dashLengthSec -= dashLength;
        this.dashCooldown += dashWait;
    }

    IEnumerator DashWait(float seconds)
    {
        _dashEnabled = false;
        yield return new WaitForSeconds(seconds);
        _dashEnabled = true;
    }

}
