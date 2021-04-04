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
    private Rigidbody2D _rg;
    private Dash _dash;

    //sonido
    public GameObject[] SonidoPlayer;
    
    public GameObject[] SonidoItems;


    void Start()
    {
        _rg = GetComponent<Rigidbody2D>();
        _dash = GetComponent<Dash>();

        gun.sight = sight;
        gun.container = weaponContainer;

        stats.currentHp = stats.maxHp;
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


    //Todo Mover logica al pedestal
    public void DropearArma()
    {
        //Instantiate(pickedGun, shotpos.transform.position, Quaternion.identity);
    }

    //retroceso del arma
    public void Recoil(float force)
    {
        DetectMouse();
        _rg.velocity = -_mouseDirection * force * Time.fixedDeltaTime;
    }


    public void DetectMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseDirection = mousePosition - (Vector2)transform.position;
        _mouseDirection.Normalize();
    }

    public void PlayDashSound()
    {
        Instantiate(SonidoPlayer[0], shotpos.transform.position, Quaternion.identity);
    }
    public void PlayStepSound()
    {
        //Instantiate(SonidoPlayer[4], shotpos.transform.position, Quaternion.identity);
    }
    public void DashReadySound()
    {
        Instantiate(SonidoPlayer[7], shotpos.transform.position, Quaternion.identity);
    }

    public void IncreaseLife(int amount, bool recover)
    {
        stats.maxHp += amount;

        if (recover)
            stats.currentHp = stats.maxHp;

        GameManager.Instance.gameUi.lifeControl.CambioVida(stats.currentHp);
    }

    public void ChangeStats(PlayerStats stats, float timeBetweenDashes, float dashLength, float dashWait, WeaponInfo weaponStats)
    {
        stats.ApplyStats(stats);
        _dash.ChangeStats(timeBetweenDashes, dashLength, dashWait);
        gun.ApplyChanges(weaponStats);
    }


    //detecta cuando entraste al arma del piso
    public void OnTriggerEnter2D(Collider2D c)
    {

        //TODO: mover logica a bala enemiga
        //if (c.gameObject.tag == "BulletsEnemy")
        //{
        //    hp = hp - 1;
        //    if (hp <= 0)
        //    {
        //        gameObject.GetComponent<Animator>().SetBool("muerte", true);
        //        StartCoroutine(Tiempo(1.7f));
        //        Gun.gameObject.SetActive(false);
        //        rb2d.velocity = Vector2.zero;
        //        move.CambioMuerto();

        //    }


        //    Instantiate(SonidoPlayer[3], shotpos.transform.position, Quaternion.identity);
        //    vida.CambioVida(hp);
        //}

        //TODO: Mover logica a explocion
        //if (c.gameObject.tag == "Explocion")
        //{
        //    hp = hp - 3;
        //    if (hp <= 0)
        //    {
        //        gameObject.GetComponent<Animator>().SetBool("muerte", true);
        //        StartCoroutine(Tiempo(2.3f));
        //        Gun.gameObject.SetActive(false);
        //        rb2d.velocity = Vector2.zero;
        //        move.CambioMuerto();

        //    }

        //    Instantiate(SonidoPlayer[3], shotpos.transform.position, Quaternion.identity);
        //    vida.CambioVida(hp);
        //}
        
        

        //TODO: Move to item
        //if (c.gameObject.tag == "ItemHealth")
        //{
        //    hp = 5;
        //    vida.CambioVida(hp);
        //    Instantiate(SonidoItems[1], transform.position, Quaternion.identity);
        //    Destroy(tag.gameObject);
        //}

        //TODO: Move to Screw
        //if (c.gameObject.tag == "Tornillo")
        //{
        //    Instantiate(SonidoItems[2], transform.position, Quaternion.identity);
        //    gameManager.SumarPuntos(150);
        //    Destroy(tag.gameObject);
        //}

    }

    //corrutina
    IEnumerator EnableMovementAfter(float seconds)
    {
      yield return new WaitForSeconds(seconds);
        _wait = true;
    }

    //Death Time Sound

    //IEnumerator DeathTime(float seconds)
    //{

    //    yield return new WaitForSeconds(seconds);
    //    if (variable)
    //    {
    //        Instantiate(SonidoPlayer[1], shotpos.transform.position, Quaternion.identity);
    //        variable = false;
    //    }
    //}


    IEnumerator Teleport(float seconds)
    {
        Instantiate(SonidoPlayer[5], shotpos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
