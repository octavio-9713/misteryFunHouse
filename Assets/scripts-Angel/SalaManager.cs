using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class SalaManager : MonoBehaviour
{
    public int idPuerta;

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
            Instantiate(enemy[indice], spawner[0].transform.position, Quaternion.identity);

            puerta.Cerrar(idPuerta);
            
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
        puerta.Abrir(idPuerta);
    }

    public void MuerteEnemy()
    {
        enemymuerte++;
    }

}

public static class LlamadoPuerta
{
    public static Action<int> Cerrar;

    public static Action<int> Abrir;
   
}

