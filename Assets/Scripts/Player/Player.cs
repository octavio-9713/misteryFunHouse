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
    [HideInInspector]
    public bool waitForProvoli;
    
    //variables para el ataque
    [Header ("Attack Attributes")]
    public Transform shotpos;

    [Header ("Weapon")]
    public Transform weaponContainer;
    public GameObject sight;
    public Gun gun;

    [Header("Life UI")]
    public Life lifeUI;

    [Header("Stats")]
    public PlayerStats stats = new PlayerStats();

    private List<WeaponBuff> _grabbedGunItems = new List<WeaponBuff>();
    private List<WeaponEffect> _appliedGunEffects = new List<WeaponEffect>();

    [Header("Animator")]
    public Animator animator;
    public AudioSource audioSource;

    private Vector2 _mouseDirection;
    private Rigidbody2D _rb;
    private Dash _dash;
    private Move _move;

    [Header("Move Audio")]
    public AudioClip moveSound;
    public AudioClip dashSound;
    public AudioClip dashReadySound;

    [Header("Hurt Audio")]
    public AudioClip hitSound;
    public AudioClip deathSound;

    private bool _waitForHurt = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _dash = GetComponent<Dash>();
        _move = GetComponent<Move>();

        gun.sight = sight.transform;
        gun.container = weaponContainer;

        stats.currentHp = stats.maxHp;

        lifeUI.ChangeMaxLife(stats.maxHp, true);
    }

    /////////////////// Move Methods //////////////////////////

    public void DisableMovement()
    {
        this._move.enabled = false;
        this._dash.enabled = false;
        this.gun.gameObject.SetActive(false);
        sight.SetActive(false);
        waitForProvoli = true;

        animator.SetBool("move", false);
        animator.SetBool("dash", false);
    }

    public void EnableMovement()
    {
        this._move.enabled = true;
        this._dash.enabled = true;
        this.gun.gameObject.SetActive(true);
        sight.SetActive(true);
        waitForProvoli = false;
    }

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
        audioSource.PlayOneShot(dashSound);
    }
    public void PlayStepSound()
    {
        audioSource.PlayOneShot(moveSound);
    }
    public void DashReadySound()
    {
        audioSource.PlayOneShot(dashReadySound);
    }

    /////////////////// Stats Methods //////////////////////////

    public void IncreaseLife(int amount, bool recover)
    {
        stats.maxHp += amount;

        if (recover)
            stats.currentHp = stats.maxHp;

        this.lifeUI.ChangeMaxLife(stats.currentHp, recover);
    }

    public void RecoverLife(int amount)
    {
        if (stats.currentHp < stats.maxHp)
        {
            stats.currentHp += amount;
            this.lifeUI.SetLifeTo(stats.currentHp);
        }
    }

    public void GetHurt(int damage, Vector3 damageDir, float nockback)
    {
        if (!_waitForHurt)
        {
            _waitForHurt = true;

            stats.currentHp -= damage;
            lifeUI.SetLifeTo(stats.currentHp);

            _rb.AddForce(damageDir * nockback * Time.deltaTime, ForceMode2D.Force);

            if (stats.currentHp <= 0)
                Die();

            else
                audioSource.PlayOneShot(hitSound);

            animator.SetTrigger("hurt");
            StartCoroutine(InvencibilityTime(stats.invencibilityTime));
        }
    }

    public void ChangeStats(PlayerBuffStats stats, float timeBetweenDashes, float dashLength, float dashWait, WeaponBuff weaponStats)
    {
        this.stats.ApplyStats(stats);
        _dash.ChangeStats(timeBetweenDashes, dashLength, dashWait);
        gun.ApplyChanges(weaponStats);

        _grabbedGunItems.Add(weaponStats);
    }

    public void ChangeWeapon(GameObject gun)
    {
        Destroy(this.gun.gameObject);
        
        GameObject newGun = Instantiate(gun.gameObject, weaponContainer);
        this.gun = newGun.GetComponent<Gun>();
        this.gun.sight = sight.transform;
        this.gun.container = weaponContainer;

        this.ReapplyGunItems();
    }

    private void ReapplyGunItems()
    {
        _grabbedGunItems.ForEach(item => gun.ApplyChanges(item));
        _appliedGunEffects.ForEach(effect => gun.ApplyEffect(effect));
    }

    public void ApplyWeaponEffect(WeaponEffect effect)
    {
        gun.ApplyEffect(effect);
        _appliedGunEffects.Add(effect);
    }

    /////////////////// Death/Collisions Methods //////////////////////////

    public void Die()
    {
        DisableMovement();
        death = true;
        gameObject.GetComponent<Animator>().SetBool("muerte", true);
        audioSource.PlayOneShot(deathSound);
    }

    public void Restart()
    {
        GameManager.Instance.RestarGame();
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

                ContactPoint2D[] contacts = new ContactPoint2D[5];
                Vector3 dir = (Vector2)gameObject.transform.position - contacts[0].point;
                GetHurt(1, dir, 1000);
            }
        }

        else
        {
            if (dashing)
                _dash.StopDash();
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
        //Instantiate(SonidoPlayer[5], shotpos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
