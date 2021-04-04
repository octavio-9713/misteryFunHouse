using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mira : MonoBehaviour
{

    //TODO: MOVE THIS CODE TO SHOT 
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    gameObject.GetComponent<Animator>().SetBool("disparo", true);
            
        //}
        //else
        //{
        //    gameObject.GetComponent<Animator>().SetBool("disparo", false);
        //}

        DetectMouse();
    }

    public void DetectMouse()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
         ));
    }

}
