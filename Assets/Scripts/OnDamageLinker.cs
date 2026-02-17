using UnityEngine;

public class OnDamageLinker : MonoBehaviour
{
    // Связывает Health и GlobalFlags

    [SerializeField] private Health _health;
    private GlobalFlags _flags;

    private void Start()
    {
        _flags = GlobalFlags.Instance;
        _health.OnDamaged += HandleDamage;
    }

    private void OnDestroy()
    {
        _health.OnDamaged -= HandleDamage;
    }

    private void HandleDamage(float damage, Collider2D col)
    {
        _flags.TriggerTakeDamage(damage, transform);
    }
}
