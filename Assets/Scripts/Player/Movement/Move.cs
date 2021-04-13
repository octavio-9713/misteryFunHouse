using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool moveEnabled = true;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private Player _player;

    [Header("Sight")]
    public Transform sight;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameManager.Instance.player;
        _animator = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!_player.death && moveEnabled)
        {

            float horMov = Input.GetAxisRaw("Horizontal");
            float verMov = Input.GetAxisRaw("Vertical");


            Vector2 move = new Vector2(horMov, verMov);

            if (move == Vector2.zero)
            {
                _animator.SetBool("move", false);
                _player.moving = false;

                _rb.velocity = Vector2.zero;
                _rb.angularVelocity = 0;
            }

            else
            {
                if (!_animator.GetBool("move"))
                    _animator.SetBool("move", true);

                _player.moving = true;
                _rb.AddForce(move * _player.stats.playerSpeed * Time.deltaTime);
            }
        }

        transform.rotation = sight.transform.position.x < transform.position.x ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);
    }

    public void DisableMove()
    {
        moveEnabled = false;
        _player.moving = false;
    }

    public void EnableMove()
    {
        this.moveEnabled = true;
    }
}



