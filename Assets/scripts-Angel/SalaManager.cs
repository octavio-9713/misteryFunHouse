
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaManager : MonoBehaviour
{
    private int numActivacion = 0;
    private bool activacion = false;
    private int indice = 0;

    public Puerta puerta;

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
            indice = Random.Range(0,1);
            Instantiate(enemy[indice], spawner[0].transform.position, Quaternion.identity);

            puerta.Cerrar();

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
        puerta.Abrir();
    }

    public void MuerteEnemy()
    {
        enemymuerte++;
    }

}
