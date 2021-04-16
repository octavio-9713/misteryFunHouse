using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.NextRoom();
    }
}
