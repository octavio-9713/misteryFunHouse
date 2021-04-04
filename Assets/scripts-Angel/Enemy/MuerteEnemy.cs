using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MuerteEnemy : MonoBehaviour
{

    public bool muerto = false;
 
    void Start()
    {
        
    }


    void Update()
    {
        if (muerto)
        {
            ContadorMuertes();
        }
    }

    public void ContadorMuertes()
    {
        gral.contador();
    }
}

public static class gral
{
    public static Action contador;
}