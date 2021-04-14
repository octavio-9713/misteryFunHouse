using UnityEngine;
using static UnityEngine.ParticleSystem;

public abstract class TypeEffect : MonoBehaviour
{
    [Header ("Time before effect passes")]
    public float effectTime;

    [Header("Effect Color and Particles")]
    public Color effectColor;
    public GameObject particleEffect;

    protected Enemy _enemy;
    protected SpriteRenderer _renderer;
    protected Color _enemyColor;

    protected GameObject _particle;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _enemy.appliedEffects.Add(this);

        if (particleEffect)
            _particle = Instantiate(particleEffect, _enemy.transform);
        
        _renderer = _enemy.GetComponent<SpriteRenderer>();

        _enemyColor = _renderer.color;
        _renderer.color = effectColor;

        Destroy(gameObject, effectTime);

        ApplyEffect();
    }

    protected void OnDestroy()
    {
        TypeEffect[] effects = _enemy.GetComponentsInChildren<TypeEffect>();

        if (_enemy.appliedEffects.Count <= 1)
            _renderer.color = _enemyColor;

        _enemy.appliedEffects.Remove(this);
        Destroy(_particle);
    }

    public abstract void ApplyEffect();
}
