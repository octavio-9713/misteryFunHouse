using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbital : MonoBehaviour
{
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(gameObject.transform.parent.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
