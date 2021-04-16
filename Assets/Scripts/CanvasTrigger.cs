using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    private GameObject _spawnPlace;

    [Header("Room Manager")]
    public RewardRoom manager;

    [Header("Prefab to Instance")]
    public GameObject prefab;
    public GameObject fastPrefab;

    public Curtain[] curtains;

    private void Start()
    {
        _spawnPlace = GameObject.FindGameObjectWithTag("ProvoliContainer");
    }

    private GameObject SelectPrefab()
    {
        if (GameManager.Instance.HasFastTime())
            return fastPrefab;

        return prefab;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DisableCurtains();
            
            GameObject provoliInst = Instantiate(SelectPrefab(), _spawnPlace.transform);
            ProvoliDialogTree dialog = provoliInst.GetComponentInChildren<ProvoliDialogTree>();
            dialog.dialogEvent.AddListener(manager.ActivarSala);

            Destroy(gameObject);
        }
    }

    private void DisableCurtains()
    {
        foreach (Curtain curtain in curtains)
            curtain.Despejar();
    }
}
