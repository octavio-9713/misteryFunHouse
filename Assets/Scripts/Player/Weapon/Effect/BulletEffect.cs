using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    [Header ("Effect Settings")]
    public Color bulletColor;
    public TypeEffect effect;

    private Bullet _bullet;

    void Start()
    {
        _bullet = GetComponentInParent<Bullet>();
        _bullet.GetComponent<SpriteRenderer>().color = bulletColor;
    }

}
