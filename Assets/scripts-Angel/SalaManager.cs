using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class SalaManager : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent closeEvent = new UnityEvent();

    [HideInInspector]
    public UnityEvent openEvent = new UnityEvent();

    private int numActivacion = 0;
    private bool activacion = false;

    public Cortina[] cortinas;

    public Transform[] spawner;
    public GameObject[] enemies;

    private int _cantEnemies;
    public int deathEnemies = 0;

    public GameObject rewardPrefab;
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
                GameObject en = Instantiate(enemies[i], spawner[i].transform.position, Quaternion.identity);
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

            if (rewardPrefab != null)
            {
                GameObject reward = Instantiate(rewardPrefab);
                Vector3 spawPos = new Vector3(rewardPos.transform.position.x, rewardPos.transform.position.y, reward.transform.position.z);
                Instantiate(GameManager.Instance.confeti, rewardPos.transform.position, transform.rotation);
                reward.transform.position = spawPos;
            }

            Destroy(gameObject);
        }

    }

    public void ActivarSala()
    {
        if (numActivacion == 0)
        {
            activacion = true;
            numActivacion++;
        }
       
    }

    public void DesactivarSala()
    {
        openEvent.Invoke();
    }

    public void MuerteEnemy()
    {
        deathEnemies++;
    }

}