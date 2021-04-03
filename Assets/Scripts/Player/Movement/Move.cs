using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool moveEnabled = true;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _renderer;

    [Header("Sight")]
    public Transform sight;

    [Header("Player")]
    public Player player;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!player.death && moveEnabled)
        {

            float horMov = Input.GetAxisRaw("Horizontal");
            float verMov = Input.GetAxisRaw("Vertical");


            Vector2 move = new Vector2(horMov, verMov);

            Debug.Log(move);

            if (move == Vector2.zero)
            {
                _animator.SetBool("Move", false);
                player.moving = false;
            }

            else
            {
                if (!_animator.GetBool("Move"))
                    _animator.SetBool("Move", true);

                player.moving = true;
            }

            _rb.AddForce(move * player.stats.playerSpeed * Time.deltaTime);
        }

        _renderer.flipX = sight.transform.position.x < transform.position.x;
    }
}



