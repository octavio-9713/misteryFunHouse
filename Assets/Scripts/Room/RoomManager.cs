using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent closeEvent = new UnityEvent();

    [HideInInspector]
    public UnityEvent openEvent = new UnityEvent();

    private int numActivacion = 0;
    private bool activacion = false;

    [Header("Room Settings")]
    public Curtain[] cortinas;
    public GameObject[] doorPos;
    public GameObject doorPrefab;
    public GameObject doorPrefabHor;

    private List<GameObject> spawnedDoors = new List<GameObject>();

    [Header("Enemies Settings")]
    public Transform[] enemySpawner;
    public GameObject[] enemies;

    private int deathEnemies = 0;
    private int _cantEnemies;

    [Header ("Rewards")]
    public GameObject[] rewardPrefabs;
    public GameObject rewardPos;

    public void Start()
    {
        _cantEnemies = enemies.Length;    
    }

    void Update()
    {

        if (activacion)
        {
            for (int i = 0; i < _cantEnemies; i++) {
                GameObject en = Instantiate(enemies[i], enemySpawner[i].transform.position, Quaternion.identity);
                en.GetComponent<Enemy>().deathEvent.AddListener(MuerteEnemy);
            }

            for (int i = 0; i < cortinas.Length; i++)
            {
                cortinas[i].Despejar();
            }

            closeEvent.Invoke();
            activacion = false;
        }

        if (deathEnemies == _cantEnemies)
        {
            DesactivarSala();

            float prob = Random.Range(0, 100);
            if (rewardPrefabs.Length > 0 && prob <= 40)
            {
                GameObject reward = Instantiate(rewardPrefabs[Random.Range(0, rewardPrefabs.Length)]);
                Vector3 spawPos = new Vector3(rewardPos.transform.position.x, rewardPos.transform.position.y, reward.transform.position.z);
                Instantiate(GameManager.Instance.confeti, rewardPos.transform.position, transform.rotation);
                reward.transform.position = spawPos;
            }

            Destroy(this);
        }

    }

    public void ActivarSala()
    {
        if (!activacion)
        {
            foreach (GameObject door in doorPos)
            {
                //if (door.GetComponent<DoorTrigger>().hor)
                //     spawnedDoors.Add(Instantiate(doorPrefabHor, door.transform.position, door.transform.rotation));

                // else
                //     spawnedDoors.Add(Instantiate(doorPrefab, door.transform.position, door.transform.rotation));

                door.GetComponent<BoxCollider2D>().isTrigger = false;
            }

            activacion = true;
        }
       
    }

    public void DesactivarSala()
    {
        foreach (GameObject door in doorPos)
            Destroy(door);

        openEvent.Invoke();
    }

    public void MuerteEnemy()
    {
        deathEnemies++;
    }

}