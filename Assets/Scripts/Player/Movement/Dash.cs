using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header ("Dash Settings")]
    public float startTime;

    [Header("Dash Wait Time")]
    public float dashWait = 0.35f;

    private Player _player;
    private Rigidbody2D _rb;
    private Animator _animator;

    private bool _dashSound = false;

    private bool _dashing = false;
    private bool _dashing2 = false;

    private float _dashTimeCounter;

    private bool _dashEnabled = true;

    private Vector2 _directionFromMouse;

    // Start is called before the first frame update
    void Start()
    {
        _dashTimeCounter = startTime;

        _rb = GetComponent<Rigidbody2D>();
        _player = this.GetComponent<Player>();
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
            _dashSound = true;
            StartCoroutine(EnableMovementAfter(dashWait));

            if (_dashing2)
            {
                _rb.AddForce(move * _player.stats.initialDashForce * Time.deltaTime);
                _player.PlayDashSound();
            }
        }
        else
        {
            if (_dashEnabled)
            {
                _dashSound = true;
                PrepareDash();
                _animator.SetBool("dash", true);
                _player.PlayDashSound();
            }
        }
    }

    private void CheckTime()
    {
        if (_dashTimeCounter <= 0f)
        {
            _dashing = false;
            _dashTimeCounter = startTime;
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
            StartCoroutine(DashWait(2.5f));
        }

    }

    void PrepareDash()
    {
        _dashing = true;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _directionFromMouse = mousePosition - (Vector2)transform.position;
        _directionFromMouse.Normalize();

        //Rotate Character
        float angleToRotate = Mathf.Atan2(_directionFromMouse.y, _directionFromMouse.x) * Mathf.Rad2Deg;
        _rb.rotation = angleToRotate;
    }

    //corrutina
    IEnumerator EnableMovementAfter(float seconds)
    {
        _dashing2 = true;
        _animator.SetBool("dash", true);

        yield return new WaitForSeconds(seconds);

        _animator.SetBool("dash", false);
        StartCoroutine(DashWait(2.5f));
        _dashing2 = false;
    }

    IEnumerator DashWait(float seconds)
    {
        _dashEnabled = false;
        yield return new WaitForSeconds(seconds);
        _dashEnabled = true;
    }

}
