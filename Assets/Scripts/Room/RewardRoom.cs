using System.Collections.Generic;
using UnityEngine;

public class RewardRoom : MonoBehaviour
{

    private int numActivacion = 0;
    private bool activacion = false;

    [Header("Room Settings")]
    public Curtain[] cortinas;
    public GameObject[] doorPos;
    public GameObject[] confetiPos;

    public GameManager.TypeOfItems itemType = GameManager.TypeOfItems.ALL;

    public GameObject itemSpawnPos;
    private GameObject spawnedItem;

    void Update()
    {

        if (activacion)
        {
            for (int i = 0; i < cortinas.Length; i++)
            {
                cortinas[i].Despejar();
            }

            foreach (GameObject pos in confetiPos)
            {
                GameObject confeti = Instantiate(GameManager.Instance.confeti, pos.transform.position, pos.transform.rotation);
                GameManager.Instance.AddInstantiatedObject(confeti);
            }

            foreach (GameObject door in doorPos)
            {
                door.SetActive(false);
            }

            activacion = false;

            GameObject item = GameManager.Instance.GetItem(itemType);
            spawnedItem = Instantiate(item, itemSpawnPos.transform);
            GameManager.Instance.AddInstantiatedObject(spawnedItem);

        }
    }

    public void ActivarSala()
    {
        if (!activacion)
        {
            activacion = true;
            numActivacion++;
        }
       
    }

    public void RestoreRoom()
    {
        if (spawnedItem)
            Destroy(spawnedItem);


        for (int i = 0; i < cortinas.Length; i++)
        {
            cortinas[i].Replegar();
        }

        foreach (GameObject door in doorPos)
        {
            door.SetActive(true);
        }

        gameObject.SetActive(true);
    }

}