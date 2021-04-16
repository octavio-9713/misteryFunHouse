using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openSide = 0;

    //1 Need Up door
    //2 Need Right door
    //3 Need Botton door
    //4 Need Left door

    private RoomTemplates _templates;
    private int _rand;
    private bool _spawned = false;

    void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f); 
    }

}
