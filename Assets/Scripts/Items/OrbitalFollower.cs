using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalFollower : MonoBehaviour
{
    Player _player;

    void Start()
    {
        _player = GameManager.Instance.player;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position;
    }
}
