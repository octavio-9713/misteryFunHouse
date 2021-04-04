using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEvent : MonoBehaviour
{
    private int entro = 0;
    public SalaManager salaManager;

    void Start()
    {}


    void Update()
    {}

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (entro == 0)
            {
                salaManager.ActivarSala();
                entro++;
            }
        }
    }


}
