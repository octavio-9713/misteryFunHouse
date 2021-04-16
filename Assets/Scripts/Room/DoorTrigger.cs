using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public RoomManager manager;
    public RewardRoom rewardManager;
    public bool hor = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("PLAYER ENTERED");
        if (collision.CompareTag("Player"))
        {
            if (manager)
                manager.ActivarSala();

            else
                rewardManager.ActivarSala();
        }
    }
}
