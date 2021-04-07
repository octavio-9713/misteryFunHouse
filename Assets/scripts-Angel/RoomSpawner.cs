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
            if (openSide == 1)
            {
                _rand = Random.Range(0, GameManager.Instance.finishSpawn ? _templates.topRoomsFinish.Length : _templates.topRooms.Length);

                GameObject sala = GameManager.Instance.finishSpawn ? _templates.topRoomsFinish[_rand] : _templates.topRooms[_rand];

                Instantiate(sala, transform.position, sala.transform.rotation);
            }
            else if (openSide == 2)
            {
                _rand = Random.Range(0, GameManager.Instance.finishSpawn ? _templates.rightRoomsFinish.Length : _templates.rightRooms.Length);

                GameObject sala = GameManager.Instance.finishSpawn ? _templates.rightRoomsFinish[_rand] : _templates.rightRooms[_rand];

                Instantiate(sala, transform.position, sala.transform.rotation);

            }
            else if (openSide == 3)
            {
                _rand = Random.Range(0, GameManager.Instance.finishSpawn ? _templates.bottomRoomsFinish.Length : _templates.bottomRooms.Length);

                GameObject sala = GameManager.Instance.finishSpawn ? _templates.bottomRoomsFinish[_rand] : _templates.bottomRooms[_rand];

                Instantiate(sala, transform.position, sala.transform.rotation);

            }
            else if (openSide == 4)
            {
                _rand = Random.Range(0, GameManager.Instance.finishSpawn ? _templates.leftRoomsFinish.Length : _templates.leftRooms.Length);

                GameObject sala = GameManager.Instance.finishSpawn ? _templates.leftRoomsFinish[_rand] : _templates.leftRooms[_rand];

                Instantiate(sala, transform.position, sala.transform.rotation);

            }

            _spawned = true;

            GameManager.Instance.IncreaseRoom();


        }
        
    }

    private void OnTriggerEnter2D(Collider2D sala)
    {      
        if (sala.CompareTag("SpawnPointSala"))
        {
            Destroy(gameObject); 
        }
    }

}
