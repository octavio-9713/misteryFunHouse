using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1Gun : MonoBehaviour
{
    public Transform ContArma;
    public Transform shotpos;
    public Transform player;
    public GameObject pistolBullet;

    //sonido
    public GameObject SonidoShoot;

    void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    
    void Update()
    {
     
    }


    //para que gire el arma
    public void LateUpdate()
    {
        //ContArma.up = ContArma.position - player.transform.position;
    }


    public void Disparar()
    {
        Instantiate(pistolBullet, shotpos.transform.position, Quaternion.identity);
        Instantiate(SonidoShoot, transform.position, Quaternion.identity);
    }
}
