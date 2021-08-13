using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public bool moveEnabled = true;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private Player _player;

    private Vector2 move; 

    [Header("Sight")]
    public Transform sight;

    public float rotationOffset = 1f;

    private float _lastRotation = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!_player.death && moveEnabled)
        { 

            if (move == Vector2.zero)
            {
                _animator.SetBool("move", false);
                _player.moving = false;

                //_rb.velocity = Vector2.zero;
                //_rb.angularVelocity = 0;
            }

            else
            {
                if (!_animator.GetBool("move"))
                    _animator.SetBool("move", true);

                _player.moving = true;
                _rb.AddForce(move * _player.stats.playerSpeed * Time.deltaTime);
            }
        }

        if (_lastRotation > 0.5f)
        {
            transform.rotation = sight.transform.position.x < (transform.position.x - rotationOffset) ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);
            _lastRotation = 0;
        }

        _lastRotation += Time.deltaTime;
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

    #region inputFunctions

    public void OnMovement(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    #endregion
}



