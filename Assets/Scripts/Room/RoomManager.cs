using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent closeEvent = new UnityEvent();

    [HideInInspector]
    public UnityEvent openEvent = new UnityEvent();

    private float _waitToAttack = 1f;
    private bool activacion = false;
    private bool deactivated = false;

    [Header("Room Settings")]
    public Curtain[] cortinas;
    public DoorTrigger[] doors;

    [Header("Enemies Settings")]
    public Transform[] enemySpawner;
    public GameObject[] enemies;

    private int deathEnemies = 0;
    private int _cantEnemies;
    private List<Enemy> _spawnedEnemies = new List<Enemy>();

    [Header ("Rewards")]
    public GameObject[] rewardPrefabs;
    public GameObject rewardPos;

    public void Start()
    {
        _cantEnemies = enemies.Length;    
    }

    void Update()
    {
        if (!deactivated)
        {
            if (activacion)
            {
                for (int i = 0; i < _cantEnemies; i++)
                {
                    GameObject en = Instantiate(enemies[i], enemySpawner[i].transform.position, Quaternion.identity);
                    Enemy enemy = en.GetComponent<Enemy>();
                    enemy.deathEvent.AddListener(MuerteEnemy);
                    enemy.WaitToWake(_waitToAttack);
                    _spawnedEnemies.Add(enemy);
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
            }
        }

    }

    public void ActivarSala()
    {
        if (!activacion)
        {
            foreach (DoorTrigger door in doors)
            {
                //if (door.GetComponent<DoorTrigger>().hor)
                //     spawnedDoors.Add(Instantiate(doorPrefabHor, door.transform.position, door.transform.rotation));

                // else
                //     spawnedDoors.Add(Instantiate(doorPrefab, door.transform.position, door.transform.rotation));
                door.LockDoor();
            }

            activacion = true;
        }
       
    }

    public void DesactivarSala()
    {
        foreach (DoorTrigger door in doors)
            door.UnlockDoor();

        openEvent.Invoke();
        deactivated = true;
        _spawnedEnemies.Clear();
    }

    public void RestoreRoom()
    {
        if (_spawnedEnemies.Count > 0)
            _spawnedEnemies.ForEach(enemy => {
                if (enemy != null)
                    Destroy(enemy.gameObject);
            });

        foreach (DoorTrigger door in doors)
        {
            door.UnlockDoor();
        }

        for (int i = 0; i < cortinas.Length; i++)
        {
            cortinas[i].Replegar();
        }

        deactivated = false;
        deathEnemies = 0;
        _spawnedEnemies.Clear();

    }

    public void MuerteEnemy()
    {
        deathEnemies++;
    }

}