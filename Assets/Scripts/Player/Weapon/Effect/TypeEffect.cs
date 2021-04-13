using UnityEngine;
using static UnityEngine.ParticleSystem;

public abstract class TypeEffect : MonoBehaviour
{
    [Header ("Time before effect passes")]
    public float effectTime;

    [Header("Effect Color and Particles")]
    public Color effectColor;
    public GameObject particleEffect;

    private Enemy _enemy;
    private SpriteRenderer _renderer;
    private Color _enemyColor;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();

        TypeEffect oldEffect = _enemy.GetComponentInChildren<TypeEffect>();

        if (oldEffect != null)
            Destroy(oldEffect.gameObject);

        else
            Instantiate(particleEffect, _enemy.transform);
        
        _renderer = _enemy.GetComponent<SpriteRenderer>();

        Debug.Log("renderer: " + _renderer);
        _enemyColor = _renderer.color;
        _renderer.color = _enemyColor;

        Destroy(this, effectTime);
    }

    private void OnDestroy()
    {
        _renderer.color = _enemyColor;
    }

    public abstract void ApplyEffect();
}
