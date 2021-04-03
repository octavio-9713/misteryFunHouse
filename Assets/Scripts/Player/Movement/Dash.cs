using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header ("Dash Settings")]
    public float startTime;

    [Header("Dash Wait Time")]
    public float dashWait = 0.35f;
    public float dashWaitTime = 2.5f;

    private Player _player;
    private Move _move;
    private Rigidbody2D _rb;
    private Animator _animator;

    private float _ogRotation;
    private bool _dashSound = false;

    private bool _dashing = false;

    private float _dashTimeCounter;

    private bool _dashEnabled = true;

    private Vector2 _directionFromMouse;

    // Start is called before the first frame update
    void Start()
    {
        _dashTimeCounter = startTime;

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
            _dashSound = true;
            PrepareDash();
            _animator.SetBool("dash", true);
            _player.PlayDashSound();
        }
    }

    private void CheckTime()
    {
        if (_dashTimeCounter <= 0f)
        {
            _dashing = false;
            _player.dashing = false;

            _dashTimeCounter = startTime;

            _animator.SetBool("dash", false);
            _move.EnableMove();
        }
        else
        {
            _dashTimeCounter -= Time.deltaTime;
        }
    }


    public void FixedUpdate()
    {
        if (_dashing)
        {
            _rb.velocity = _directionFromMouse * _player.stats.dashSpeed * Time.fixedDeltaTime;
            StartCoroutine(DashWait(dashWaitTime));
        }

    }

    void PrepareDash()
    {
        _dashing = true;
        _player.dashing = true;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _directionFromMouse = mousePosition - (Vector2)transform.position;
        _directionFromMouse.Normalize();
    }

    IEnumerator DashWait(float seconds)
    {
        _dashEnabled = false;
        yield return new WaitForSeconds(seconds);
        _dashEnabled = true;
    }

}
