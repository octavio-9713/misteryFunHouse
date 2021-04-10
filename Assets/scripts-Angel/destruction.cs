using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D sala)
    {
        Destroy(sala.gameObject);
    }
}
