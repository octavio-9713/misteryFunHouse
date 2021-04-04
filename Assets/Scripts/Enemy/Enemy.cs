using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    [HideInInspector]
    public UnityEvent deathEvent = new UnityEvent();

    public GameObject[] SonidoEnemy;
    protected bool variable = true;

    public IEnumerator muerte(float seconds)
    {
        SonidoMuerte();
        deathEvent.Invoke();
        yield return new WaitForSeconds(seconds);

    }

    // el sonido de la muerte
    private void SonidoMuerte()
    {

        if (variable)
        {
            Instantiate(SonidoEnemy[0], transform.position, Quaternion.identity);
            variable = false;
        }
    }
}
