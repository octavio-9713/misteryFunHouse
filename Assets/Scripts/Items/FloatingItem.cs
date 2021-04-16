using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 _newPosition = transform.position;
        _newPosition.y += Mathf.Sin(Time.time) * Time.deltaTime;
        transform.position = _newPosition;
    }
}
