using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Weapon Shot Position")]
    public Transform shotpos;

    [Header("Weapon Settings")]
    public WeaponInfo weapon;

    [HideInInspector]
    public Transform sight;
    [HideInInspector]
    public Transform container;

    private Player _player;
    private bool _canShoot = true;

    void Start()
    {
        _player = GameManager.Instance.player;
    }


    void Update()
    {
        DetectMouse();

        if (Input.GetMouseButton(0) && _canShoot)
            Shoot();

        if (sight.transform.position.y > container.transform.position.y)
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2;
        
        else
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

    public void Shoot()
    {
        for (int i = 0; i < weapon.bulletQuantity; i++)
        {
            Instantiate(weapon.bullet, shotpos.transform.position, transform.rotation);
        }

        Instantiate(weapon.weaponSound, shotpos.transform.position, Quaternion.identity);
        StartCoroutine(WaitToShoot(1.3f));
    }


    public void LateUpdate()
    {
        container.up = container.position - sight.position;
    }


    //detectar el mause
    public void DetectMouse()
    {
        sight.position = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Camera.main.transform.position.z
            ));
    }

    IEnumerator WaitToShoot(float seconds)
    {
        _canShoot = false;
        yield return new WaitForSeconds(seconds);
        _canShoot = true;
    }
}