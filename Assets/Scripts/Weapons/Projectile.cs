using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Damage _damage;
    private float _speed;
    private Vector3 _direction;

    // ¬¿∆ÕŒ: ◊“Œ-¡€ œ”À» »√–Œ ¿ Õ≈ ƒ¿Ã¿∆»À» ≈√Œ

    public void Initialize(Damage damage, float speed, Vector3 direction)
    {
        _damage = damage;
        _speed = speed;
        _direction = direction.normalized;
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health targetHealth = collision.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}