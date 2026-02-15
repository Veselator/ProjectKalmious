using UnityEngine;

public class HealthRegen : BaseAbility
{
    [SerializeField] private float _regenAmount = 10f;
    [SerializeField] private float _regenTime = 2f;
    
    private IHealth _health;
    private Timer _regenCooldown = new();

    public override void Initialize(Transform ownerTransform, AbilitiesManager manager, Collider2D ownerCollider, AbilitySO data)
    {
        base.Initialize(ownerTransform, manager, ownerCollider, data);
        _health = manager.LinkedHealth;
    }

    private void Update()
    {
        if (!_regenCooldown.IsRunning) return;

        if (_health.IsDamaged)
        {
            _health.CurrentHealth += _regenAmount * Time.deltaTime;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _regenCooldown.Disable();
    }

    protected override void Act()
    {
        _regenCooldown.Start(_regenTime);
    }
}
