using System;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour, IAbility
{
    [SerializeField] protected float _cooldownTime = 1f;

    protected Transform _ownerTransform;
    protected AbilitiesManager _manager;
    protected Collider2D _ownerCollider;
    protected Timer _cooldown;
    protected bool _isReady = true;

    public Timer Cooldown => _cooldown;
    public event Action<IAbility> OnAct;

    public virtual void Initialize(Transform ownerTransform, AbilitiesManager manager, Collider2D ownerCollider)
    {
        _ownerTransform = ownerTransform;
        _manager = manager;
        _ownerCollider = ownerCollider;
        _cooldown = new Timer();
        _cooldown.OnEnd += () => _isReady = true;
    }

    public virtual bool CanDo()
    {
        return _isReady;
    }

    public virtual void Do()
    {
        if (!CanDo()) return;
        _isReady = false;
        _cooldown.Start(_cooldownTime);
        Act();
        OnAct?.Invoke(this);
    }

    protected abstract void Act();

    protected virtual void OnDestroy()
    {
        _cooldown?.Disable();
    }
}