using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header ("Managers")]
    public RoomManager manager;
    public RewardRoom rewardManager;

    private BoxCollider2D _collider;
    
    public GameObject door;

    public void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PLAYER ENTERED");
        if (collision.CompareTag("Player"))
        {
            if (manager)
            {
                manager.ActivarSala();
            }

            else
                rewardManager.ActivarSala();
        }
    }

    public void LockDoor()
    {
        Destroy(_collider);
        door.SetActive(true);
    }

    public void UnlockDoor()
    {
        Destroy(door);
        Destroy(gameObject);
    }

}
