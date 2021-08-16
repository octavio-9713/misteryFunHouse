using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject target;
    private Vector3 targetPos;

    public float smoothin;

    //detectar el mause
    private Vector2 directionFromMouse;
    public float _distanceToPlayer;

    private Player _player;
    
    public Transform sight;

    void Start()
    {
        _player = target.GetComponent<Player>();
        _distanceToPlayer = transform.position.z;
    }

    
    void Update()
    {
        if (!_player.waitForProvoli && !_player.death)
        {
            DetectarMouse();

            targetPos = new Vector3(target.transform.position.x, target.transform.position.y, _distanceToPlayer);

            targetPos = new Vector3(targetPos.x + directionFromMouse.x * 30, targetPos.y + directionFromMouse.y * 30, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothin * Time.deltaTime);
        }

        else if (_player.death)
        {

            targetPos = new Vector3(_player.transform.position.x, _player.transform.position.y, _distanceToPlayer * (0.25f));
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothin * Time.deltaTime);

        }
    }



    public void DetectarMouse()
    {
        Vector2 mousePosition = this.sight.transform.position;
        directionFromMouse = mousePosition - (Vector2)transform.position;
        directionFromMouse.Normalize();
    }
}
