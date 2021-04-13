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

    public void Spawn()
    {
        if (_spawned == false)
        {
            if (GameManager.Instance.finishSpawn)
                SpawnEnd();

            else
            {
                float probEnd = Random.Range(0, 100);
                if (probEnd < 20)
                    SpawnEnd();

                else
                    SpawnNormal();
    
                GameManager.Instance.IncreaseRoom();
            }

            _spawned = true;
        }
        
    }

    private void SpawnNormal()
    {
        GameObject[] salas = openSide == 1 ? _templates.topRooms : openSide == 2 ?
            _templates.rightRooms : openSide == 3 ? _templates.bottomRooms : _templates.leftRooms;


        _rand = Random.Range(0, salas.Length - 1);

        Debug.Log(openSide);
        Debug.Log("Quantity: " + _rand);
        GameObject sala = salas[_rand];
        Instantiate(sala, transform.position, sala.transform.rotation, transform.parent.parent.parent);
    }

    private void SpawnEnd()
    {
        GameObject[] salas = openSide == 1 ? _templates.topRoomsFinish : openSide == 2 ?
            _templates.rightRoomsFinish : openSide == 3 ? _templates.bottomRoomsFinish : _templates.leftRoomsFinish;


        _rand = Random.Range(0, salas.Length - 1);
        GameObject sala = salas[_rand];
        GameObject spawnedRoom = Instantiate(sala, transform.position, sala.transform.rotation, transform.parent.parent.parent);
        GameManager.Instance.AddEndRoom(openSide, spawnedRoom);
    }

    private void OnTriggerEnter2D(Collider2D sala)
    {      
        if (sala.CompareTag("SpawnPointSala"))
        {
            RoomSpawner spawner = sala.GetComponent<RoomSpawner>();
            if(spawner != null && spawner._spawned == false)
            {
                Instantiate(_templates.closedRoom, transform.position, sala.transform.rotation);
                Destroy(gameObject);
            }

            _spawned = true;
            
        }
    }

}
