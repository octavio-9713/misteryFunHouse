using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    private GameObject _spawnPlace;

    [Header("Prefab to Instance")]
    public GameObject prefab;

    public Curtain[] curtains;

    private void Start()
    {
        _spawnPlace = GameObject.FindGameObjectWithTag("ProvoliContainer");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DisableCurtains();
            Instantiate(prefab, _spawnPlace.transform);
            Destroy(gameObject);
        }
    }

    private void DisableCurtains()
    {
        foreach (Curtain curtain in curtains)
            curtain.Despejar();
    }
}
