using UnityEngine;

public class ShakeCameraOnDamage : MonoBehaviour
{
    [SerializeField] private CameraShake _shake;
    [SerializeField] private Health _health;

    private void Start()
    {
        _health.OnDamaged += HandleDamaged;
    }

    private void OnDestroy()
    {
        _health.OnDamaged -= HandleDamaged;
    }

    private void HandleDamaged(float damage, Collider2D _)
    {
        _shake.StartHitShake();
    }
}
