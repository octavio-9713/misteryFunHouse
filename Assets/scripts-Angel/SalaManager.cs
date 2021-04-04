using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class SalaManager : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent closeEvent = new UnityEvent();

    [HideInInspector]
    public UnityEvent openEvent = new UnityEvent();

    private int numActivacion = 0;
    private bool activacion = false;
    private int indice = 0;

    public Puerta puerta;
    public Cortina cortina;

    public Transform[] spawner;
    public GameObject[] enemy;

    public int cantEnemy;
    private int enemymuerte = 0;

    void Start()
    {
        gral.contador += MuerteEnemy;
    }

    
    void Update()
    {

        if (activacion)
        {
            indice = UnityEngine.Random.Range(0,1);
            GameObject en = Instantiate(enemy[indice], spawner[0].transform.position, Quaternion.identity);
            en.GetComponent<Enemy>().deathEvent.AddListener(DesactivarSala);

            closeEvent.Invoke();
            cortina.Despejar();

            activacion = false;
        }

        if (enemymuerte == cantEnemy)
        {
            DesactivarSala();
        }

    }

    public void ActivarSala()
    {
        if (numActivacion == 0)
        {
            activacion = true;
            numActivacion++;
        }
       
    }

    public void DesactivarSala()
    {
        openEvent.Invoke();
    }

    public void MuerteEnemy()
    {
        enemymuerte++;
    }

}