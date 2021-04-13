using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D sala)
    {
        if ( !sala.gameObject.CompareTag("Player") ) {
            
            Destroy(sala.gameObject);
        }
    }
}
