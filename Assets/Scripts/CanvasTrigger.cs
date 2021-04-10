using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    [Header ("Canvas")]
    public GameObject spawnPlace;

    [Header("Prefab to Instance")]
    public GameObject prefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(prefab, spawnPlace.transform);
            Destroy(gameObject);
        }
    }
}
