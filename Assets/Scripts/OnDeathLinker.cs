using UnityEngine;

public class OnDeathLinker : MonoBehaviour
{
    // Связывает Health и GlobalFlags

    [SerializeField] private BaseAI _ai;
    [SerializeField] private Health _health;
    private GlobalFlags _flags;

    private void Start()
    {
        _flags = GlobalFlags.Instance;
        _health.OnDeath += HandleDamage;
    }

    private void OnDestroy()
    {
        _health.OnDeath -= HandleDamage;
    }

    private void HandleDamage()
    {
        _flags.TriggerEnemyKilled(_ai, transform.position);
    }
}
