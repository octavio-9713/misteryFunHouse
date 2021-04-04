﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyT1 : Enemy
{// Variables para gestionar el radio de visión, el de ataque y la velocidad
    public float visionRadius;
    public float attackRadius;
    public float speed;

    // Variables relacionadas con el ataque
    [Tooltip("Velocidad de ataque (segundos entre ataques)")]
    public float attackSpeed = 2f;
    bool attacking;
    public T1Gun t1Gun;

    ///--- Variables relacionadas con la vida
    [Tooltip("Puntos de vida")]
    public int maxHp = 3;
    [Tooltip("Vida actual")]
    private double hp;

    // Variable para guardar al jugador
    GameObject player;

    // Variable para guardar la posición inicial
    Vector3 initialPosition, target;

    // Animador y cuerpo cinemático con la rotación en Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    // Variable para los drop
    public GameObject pistol;
    public bool dropeoFijo;
    public GameObject tornillo;
   

    //movimiento aleatorio
    private bool movAleato = true;

    //variable para los puntos
    public int puntos = 75;
    public GameManager manager;

    //para acceder al collider;
    private BoxCollider2D m_ObjectCollider;

    //variables para la muerte
    private bool muerto = false;
    public GameObject explocionMuerte;



    void Start()
    {

        // Recuperamos al jugador gracias al Tag
        player = GameObject.FindGameObjectWithTag("Player");

        m_ObjectCollider = GetComponent<BoxCollider2D>();

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        ///--- Iniciamos la vida
        hp = maxHp;
    }

    void Update()
    {

        if (!muerto)
        {

            // Guardamos nuestra posición inicial
            initialPosition = transform.position;

            // Por defecto nuestro target siempre será nuestra posición inicial
            target = initialPosition;

            // Comprobamos un Raycast del enemigo hasta el jugador
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                player.transform.position - transform.position,
                visionRadius,
                1 << LayerMask.NameToLayer("Default")
            // Poner el propio Enemy en una layer distinta a Default para evitar el raycast
            // También poner al objeto Attack y al Prefab Slash una Layer Attack 
            // Sino los detectará como entorno y se mueve atrás al hacer ataques
            );

            // Aquí podemos debugear el Raycast
            Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
            Debug.DrawRay(transform.position, forward, Color.red);

            // Si el Raycast encuentra al jugador lo ponemos de target
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    target = player.transform.position;
                }
            }

            // Calculamos la distancia y dirección actual hasta el target
            float distance = Vector3.Distance(target, transform.position);
            Vector3 dir = (target - transform.position).normalized;

            // Si es el enemigo y está en rango de ataque nos paramos y le atacamos
            if (target != initialPosition && distance < attackRadius)
            {
                //poner la animacion de mover aca
                gameObject.GetComponent<Animator>().SetBool("move", false);


                ///-- Empezamos a atacar (importante una Layer en ataque para evitar Raycast)
                if (!attacking) StartCoroutine(Attack(attackSpeed));
            }
            // En caso contrario nos movemos hacia él
            else
            {
                rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

                //poner la animacion de mover
                gameObject.GetComponent<Animator>().SetBool("move", true);
            }

            // Una última comprobación para evitar bugs forzando la posición inicial
            if (target == initialPosition && distance < 0.05f)
            {
                // Y cambiamos la animación de nuevo a Idle
                gameObject.GetComponent<Animator>().SetBool("move", false);

                if (movAleato)
                {
                    float movimientoX = Random.Range(-1f, 1f);
                    float movimientoY = Random.Range(-1f, 1f);
                    float time = Random.Range(7f, 15f);

                    Vector2 offset = new Vector2(movimientoX, movimientoY);
                    StartCoroutine(MovimientoAleatorio(time, offset));
                }
            }



            //para que gire el personaje
            if (player.transform.position.x < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

        }
    
        

    }

    //la funcion para que caminen cuando no estan atacando
    public IEnumerator MovimientoAleatorio(float seconds, Vector2 offset)
    {

        movAleato = false;

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        rb2d.velocity = offset * 700f * Time.fixedDeltaTime;

        gameObject.GetComponent<Animator>().SetBool("move", true);

        yield return new WaitForSeconds(3f);

        rb2d.velocity = Vector2.zero;
        gameObject.GetComponent<Animator>().SetBool("move", false);

        yield return new WaitForSeconds(seconds);

        movAleato = true;

    }

    //funcion para dropear el tornillo
    public void dropeoTornillo()
    {
        int numeroRam = Random.Range(0, 4);

        for (int i = 0; i < numeroRam; i++)
        {
            float dispercionH = Random.Range(-6f, 6f);
            float dispercionHY = Random.Range(-6f, 6f);

            Vector3 offset = new Vector3(dispercionH, dispercionHY, 0f);

            Instantiate(tornillo, transform.position + offset, transform.rotation);
        }

    }

    //funcion para dropear el arma que lleva
    public void dropeoArma()
    {
        int numeroRam = Random.Range(1, 3);

        if (dropeoFijo)
        {
            Instantiate(pistol, transform.position, Quaternion.identity);
        }
        else
        {
            if (numeroRam == 2)
            {
                Instantiate(pistol, transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator Attack(float seconds)
    {
        attacking = true;  
        t1Gun.Disparar();
        yield return new WaitForSeconds(seconds);
        attacking = false; 
    }


    ///--- Gestión del daño de las armas y la vida
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BuDefault" && !muerto)
        {
            hp = hp - 1;
            if (hp == 0)
            {
                muerto = true;
                m_ObjectCollider.isTrigger = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                t1Gun.gameObject.SetActive(false);
                Instantiate(explocionMuerte, transform.position, Quaternion.identity);
                Destroy(gameObject);

                StartCoroutine(muerte(2.25f));
                dropeoArma();
                dropeoTornillo();
                manager.SumarPuntos(puntos);
            }

            Instantiate(SonidoEnemy[1], transform.position, Quaternion.identity);
        }

        if (collision.gameObject.tag == "BuPistol" && !muerto)
        {
            hp = hp - 1;
            if (hp <= 0)
            {
                muerto = true;
                m_ObjectCollider.isTrigger = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                t1Gun.gameObject.SetActive(false);
                Instantiate(explocionMuerte, transform.position, Quaternion.identity);
                Destroy(gameObject);

                StartCoroutine(muerte(2.25f));
                dropeoArma();
                dropeoTornillo();
                manager.SumarPuntos(puntos);
            }

            Instantiate(SonidoEnemy[1], transform.position, Quaternion.identity);
        }

        if (collision.gameObject.tag == "BuMachine" && !muerto)
        {
            hp = hp - 0.5;
            if (hp <= 0)
            {
                muerto = true;
                m_ObjectCollider.isTrigger = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                t1Gun.gameObject.SetActive(false);
                Instantiate(explocionMuerte, transform.position, Quaternion.identity);
                Destroy(gameObject);

                StartCoroutine(muerte(2.25f));
                dropeoArma();
                dropeoTornillo();
                manager.SumarPuntos(puntos);
            }

            Instantiate(SonidoEnemy[1], transform.position, Quaternion.identity);
        }

        if (collision.gameObject.tag == "BuShotgun" && !muerto)
        {
            hp = hp - 0.5;
            if (hp <= 0)
            {
                muerto = true;
                m_ObjectCollider.isTrigger = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                t1Gun.gameObject.SetActive(false);
                Instantiate(explocionMuerte, transform.position, Quaternion.identity);
                Destroy(gameObject);

                StartCoroutine(muerte(2.25f));
                dropeoArma();
                dropeoTornillo();
                manager.SumarPuntos(puntos);
            }

            Instantiate(SonidoEnemy[1], transform.position, Quaternion.identity);
        }

        if (collision.gameObject.tag == "Explocion" && !muerto)
        {
            hp = hp - 3;
            if (hp <= 0)
            {
                muerto = true;
                m_ObjectCollider.isTrigger = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                t1Gun.gameObject.SetActive(false);
                Instantiate(explocionMuerte, transform.position, Quaternion.identity);

                StartCoroutine(muerte(2.25f));
                dropeoArma();
                dropeoTornillo();
                manager.SumarPuntos(puntos);
            }

            Instantiate(SonidoEnemy[1], transform.position, Quaternion.identity);
        }




    }

    public IEnumerator muerte(float seconds)
    {
        SonidoMuerte();
        deathEvent.Invoke();
         yield return new WaitForSeconds(seconds);
        
    }

    // el sonido de la muerte
    private void SonidoMuerte()
    {
        
        if (variable)
        {
            Instantiate(SonidoEnemy[0], transform.position, Quaternion.identity);
            variable = false;
        }
    }
}

