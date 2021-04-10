using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [HideInInspector]
    public bool death;
    [HideInInspector]
    public bool moving;
    [HideInInspector]
    public bool dashing;
    
    //variables para el ataque
    [Header ("Attack Attributes")]
    public Transform shotpos;

    [Header ("Weapon")]
    public Transform weaponContainer;
    public Transform sight;
    public Gun gun;

    [Header("Life UI")]
    public Life lifeUI;

    [Header("Stats")]
    public PlayerStats stats = new PlayerStats();

    private bool _wait = true;

    private Vector2 _mouseDirection;
    private Rigidbody2D _rb;
    private Dash _dash;
    private Move _move;
    private AudioSource _audio;

    //sonido
    public GameObject[] SonidoPlayer;
    
    public GameObject[] SonidoItems;

    private bool _waitForHurt = false;

    private List<WeaponInfo> gunApliedStats = new List<WeaponInfo>();

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _dash = GetComponent<Dash>();
        _move = GetComponent<Move>();
        _audio = GetComponent<AudioSource>();

        gun.sight = sight;
        gun.container = weaponContainer;

        stats.currentHp = stats.maxHp;

        lifeUI = GameManager.Instance.gameUi.GetComponentInChildren<Life>();
    }

    
    void Update()
    {
        //TODO PickUP Logic 
        //if (tag.gameObject.CompareTag("default") && entro)
        //{
        //    if (Input.GetKey("e") && _wait)
        //    {
        //        _wait = false;
        //        DropearArma();
        //        indi = 0;
        //        Instantiate(SonidoPlayer[2], shotpos.transform.position, Quaternion.identity); 


        //        Uiarma(indi);
        //        Destroy(tag.gameObject);

        //        StartCoroutine(EnableMovementAfter(0.5f));

        //    }

        //}

        // TODO: Mover logica a portal
        //if (tag.gameObject.CompareTag("Portal") && entro)
        //{
        //    if (confettiBool)
        //    {               
        //        StartCoroutine(Confetti(0.4f));
        //        confettiBool = false;
        //    }

        //    if (Input.GetKey("e") && TP)
        //    {

        //        StartCoroutine(Teleport(0.15f));
        //        TP = false;
        //    }


        //}
    }

    /////////////////// Move Methods //////////////////////////

    public Vector2 MovingDirection()
    {
        float horMov = Input.GetAxisRaw("Horizontal");
        float verMov = Input.GetAxisRaw("Vertical");

        return new Vector2(horMov, verMov);
    }

    //retroceso del arma
    public void Recoil(float force)
    {
        DetectMouse();
        _rb.velocity = -_mouseDirection * force * Time.fixedDeltaTime;
    }


    public void DetectMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseDirection = mousePosition - (Vector2)transform.position;
        _mouseDirection.Normalize();
    }

    /////////////////// Sound Methods //////////////////////////

    public void PlayDashSound()
    {
        Instantiate(SonidoPlayer[0], shotpos.transform.position, Quaternion.identity);
    }
    public void PlayStepSound()
    {
        Instantiate(SonidoPlayer[4], shotpos.transform.position, Quaternion.identity);
    }
    public void DashReadySound()
    {
        Instantiate(SonidoPlayer[7], shotpos.transform.position, Quaternion.identity);
    }

    /////////////////// Stats Methods //////////////////////////

    public void IncreaseLife(int amount, bool recover)
    {
        stats.maxHp += amount;

        if (recover)
            stats.currentHp = stats.maxHp;

        GameManager.Instance.gameUi.lifeControl.CambioVida(stats.currentHp);
    }

    public void RecoverLife(int amount)
    {
        if (stats.currentHp < stats.maxHp)
        {
            stats.currentHp += amount;
            GameManager.Instance.gameUi.lifeControl.CambioVida(stats.currentHp);
        }
    }

    public void GetHurt(int damage, Vector3 damageDir)
    {
        if (!_waitForHurt)
        {
            stats.currentHp -= damage;

            _rb.AddForce(damageDir.normalized * 75000 * Time.deltaTime);

            if (stats.currentHp <= 0)
                Die();

            Instantiate(SonidoPlayer[3], shotpos.transform.position, Quaternion.identity);
            lifeUI.CambioVida(stats.currentHp);

            StartCoroutine(InvencibilityTime(stats.invencibilityTime));
        }
    }

    public void ChangeStats(PlayerStats stats, float timeBetweenDashes, float dashLength, float dashWait, WeaponInfo weaponStats)
    {
        stats.ApplyStats(stats);
        _dash.ChangeStats(timeBetweenDashes, dashLength, dashWait);
        gun.ApplyChanges(weaponStats);
        gunApliedStats.Add(weaponStats);
    }

    /////////////////// Death/Collisions Methods //////////////////////////

    public void Die()
    {
        gameObject.GetComponent<Animator>().SetBool("muerte", true);
        Instantiate(SonidoPlayer[1], shotpos.transform.position, Quaternion.identity);
        gun.gameObject.SetActive(false);
        _rb.velocity = Vector2.zero;
        _move.DisableMove();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (dashing)
            {
                Vector3 dir = collision.gameObject.transform.position - this.transform.position;
                collision.gameObject.GetComponent<Enemy>().GetHit(stats.dashDamage, dir.normalized);

                _dash.StopDash();
            }

            else
            {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = 0;

                Vector3 dir = this.transform.position - collision.gameObject.transform.position;
                GetHurt(1, dir);
            }
        }
    }

    //corrutina
    IEnumerator InvencibilityTime(float seconds)
    {
        _waitForHurt = true;
        yield return new WaitForSeconds(seconds);
        _waitForHurt = false;
    }


    IEnumerator Teleport(float seconds)
    {
        Instantiate(SonidoPlayer[5], shotpos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
