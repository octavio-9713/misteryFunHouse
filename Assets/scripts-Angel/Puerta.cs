using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
   
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Cerrar()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        return;
    }

    public void Abrir()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        return;
    }
}
