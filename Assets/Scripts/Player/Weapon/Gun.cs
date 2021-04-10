using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Weapon Shot Position")]
    public Transform shotpos;

    [Header("Weapon Settings")]
    public WeaponInfo weapon = new WeaponInfo();
    public Transform sight;
    public Transform container;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shootAudio;

    private Player _player;
    private bool _canShoot = true;

    void Start()
    {
        _player = GameManager.Instance.player;
        Debug.Log(weapon.weaponSprite);
        GameManager.Instance.gameUi.ChangePanel(weapon.weaponSprite);
    }


    void Update()
    {
        DetectMouse();

        if (Input.GetButton("Fire1") && _canShoot)
            Shoot();

        if (sight.transform.position.y > container.transform.position.y)
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2;
        
        else
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

    public void Shoot()
    {
        StartCoroutine(InstantiateShoot());
        StartCoroutine(WaitToShoot(weapon.weaponCooldown));
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

    public void ApplyChanges(WeaponInfo weaponInfo)
    {
        this.weapon.ApplyChanges(weaponInfo);
    }

    IEnumerator WaitToShoot(float seconds)
    {
        _canShoot = false;
        yield return new WaitForSeconds(seconds);
        _canShoot = true;
    }

    IEnumerator InstantiateShoot()
    {
        for (int i = 0; i < weapon.bulletQuantity; i++)
        {
            GameObject instance = Instantiate(weapon.bullet, shotpos.transform.position, transform.rotation);
            Bullet bullet = instance.GetComponent<Bullet>();

            bullet.speed = weapon.bulletSpeed;
            bullet.damage = weapon.weaponDamage;
            bullet.life = weapon.bulletLife;

            Instantiate(weapon.weaponSound, shotpos.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(weapon.weaponCadence);
        }

        this.audioSource.PlayOneShot(shootAudio);
    }
}