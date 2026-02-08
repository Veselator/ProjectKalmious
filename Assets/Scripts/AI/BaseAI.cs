using UnityEngine;

public abstract class BaseAI : MonoBehaviour
{
    [SerializeField] protected IMovement _movement;
    [SerializeField] protected float _updateInterval = 0.1f;

    protected float _lastUpdateTime;
    protected bool _isActive = true;

    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

    protected virtual void Awake()
    {
        if (_movement == null)
        {
            _movement = GetComponent<IMovement>();
        }
    }

    protected virtual void Update()
    {
        if (!_isActive) return;

        if (Time.time >= _lastUpdateTime + _updateInterval)
        {
            _lastUpdateTime = Time.time;
            UpdateAI();
        }
    }

    protected abstract void UpdateAI();

    public virtual void Stop()
    {
        _movement?.Stop();
        _isActive = false;
    }

    public virtual void Resume()
    {
        _isActive = true;
    }
}