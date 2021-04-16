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
                Instantiate(GameManager.Instance.confeti, pos.transform.position, pos.transform.rotation);
            }

            activacion = false;

            GameObject item = GameManager.Instance.GetItem(itemType);
            Instantiate(item, itemSpawnPos.transform);

            Destroy(this);
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

}