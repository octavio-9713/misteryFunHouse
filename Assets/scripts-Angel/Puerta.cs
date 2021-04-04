using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Puerta : MonoBehaviour
{
    private SalaManager _manager;

    void Start()
    {
        _manager = GetComponentInParent<SalaManager>();
        _manager.closeEvent.AddListener(Cerrar);
        _manager.openEvent.AddListener(Abrir);
    }

    public void Cerrar()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
     }

    public void Abrir()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;   
    }
}
