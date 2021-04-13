using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRewardTrigger : MonoBehaviour
{
    public RewardRoom manager;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            manager.ActivarSala();
    }
}
