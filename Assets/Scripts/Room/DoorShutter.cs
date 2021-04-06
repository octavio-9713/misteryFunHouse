using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorShutter : MonoBehaviour
{
    private int entro = 0;
    public RoomManager salaManager;

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
