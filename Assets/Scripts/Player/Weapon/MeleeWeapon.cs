using UnityEngine;

public class MeleeWeapon : Gun
{
    private Animator _playerAnimator;

    public MeleeHit hitPoint;

    void Start()
    {
        _player = GameManager.Instance.player;
        _playerAnimator = _player.GetComponent<Animator>();

        _renderer = GetComponent<SpriteRenderer>();
        GameManager.Instance.gameUi.ChangePanel(weapon.weaponSprite);

        hitPoint = container.GetComponentInChildren<MeleeHit>();
    }

    public void LateUpdate()
    {
        if (_canShoot)
            container.up = container.position - sight.position;
    }

    public void ApplyEffectToWeapon()
    {
        WeaponEffect effect = effects[Random.Range(0, effects.Count)];
        hitPoint.effect = effect.effect;
        Instantiate(effect.effect, hitPoint.transform);
    }


    public override void Shoot()
    {
        GameObject instance = Instantiate(weapon.bullet, shotpos.transform.position, shotpos.transform.rotation);
        Bullet bullet = instance.GetComponent<Bullet>();

        bullet.speed = weapon.bulletSpeed;
        bullet.damage = weapon.weaponDamage;
        bullet.life = weapon.bulletLife;

        if (SpawnBulletWithEffect())
        {
            ApplyEffectToBullet(bullet);
        }

        StartCoroutine(WaitToShoot(weapon.weaponCooldown));
    }
}
