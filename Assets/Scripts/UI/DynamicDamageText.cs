using UnityEngine;
using TMPro;

public class DynamicDamageText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _lifetime = 0.8f;
    [SerializeField] private float _gravity = 3f;
    [SerializeField] private float _horizontalSpread = 1f;
    [SerializeField] private float _initialUpForce = 2.5f;

    private Vector2 _velocity;
    private float _elapsed;
    private Color _startColor;
    private DamageTextAnimationManager _pool;

    public void Init(float damage, DamageTextAnimationManager pool)
    {
        _pool = pool;
        _elapsed = 0f;

        _text.text = Mathf.RoundToInt(damage).ToString();
        _startColor = _text.color;
        _startColor.a = 1f;
        _text.color = _startColor;

        float horizontalDir = Random.Range(-_horizontalSpread, _horizontalSpread);
        _velocity = new Vector2(horizontalDir, _initialUpForce);
    }

    private void Update()
    {
        _elapsed += Time.deltaTime;

        _velocity.y -= _gravity * Time.deltaTime;
        transform.position += (Vector3)_velocity * Time.deltaTime;

        float alpha = 1f - (_elapsed / _lifetime);
        Color color = _startColor;
        color.a = Mathf.Max(alpha, 0f);
        _text.color = color;

        if (_elapsed >= _lifetime)
            _pool.Return(gameObject);
    }
}