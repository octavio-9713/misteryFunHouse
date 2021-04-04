using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Puerta : MonoBehaviour
{
    public int idPuerta;

    void Start()
    {
        LlamadoPuerta.Cerrar += Cerrar;

        LlamadoPuerta.Abrir += Abrir;
    }


    void Update()
    {
        
    }

    public void Cerrar(int idCerrar)
    {
        
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        return;
        
    }

    public void Abrir(int idabrir)
    {
      
        
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            return;
        
    }
}
