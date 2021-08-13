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
        _collider.enabled = false;
        door.SetActive(true);

        door.TryGetComponent<Animator>(out Animator anim);
        if (anim)
            anim.SetTrigger("close");
    }

    public void UnlockDoor()
    {
        door.TryGetComponent<Animator>(out Animator anim);
        if (anim)
            anim.SetTrigger("open");

        door.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }

}
