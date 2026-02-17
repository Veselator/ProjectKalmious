using System;
using UnityEngine;

public abstract class BaseAI : MonoBehaviour
{
    // Базовый класс ИИ для движения

    [SerializeField] protected RigidbodyMovement _movement;
    [SerializeField] protected float _updateInterval = 0.1f;
    [SerializeField] protected float _stoppingDistance = 0.5f;

    protected Transform _player;

    protected float _lastUpdateTime;
    protected bool _isActive = true;
    private float _distanceToPlayer;

    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

    private GlobalFlags _globalFlags; // Проблема - связанность с конкретной реализацией глобальных флагов

    public float DistanceToPlayer => _distanceToPlayer;

    public event Action<BaseAI, Transform> OnInited;

    protected virtual void Awake()
    {
        if (_movement == null)
        {
            _movement = GetComponent<RigidbodyMovement>();
        }
    }

    public void Initialize(Transform player)
    {
        _globalFlags = GlobalFlags.Instance;
        _globalFlags.OnGameOver += Stop;

        _player = player;
        OnInited?.Invoke(this, player);
    }

    protected void OnDestroy()
    {
        if(_globalFlags != null) _globalFlags.OnGameOver -= Stop;
    }

    protected virtual void Update()
    {
        if (!_isActive) return;
        UpdateDistanceToPlayer();

        if (Time.time >= _lastUpdateTime + _updateInterval)
        {
            _lastUpdateTime = Time.time;
            UpdateAI();
        }
    }

    private void UpdateDistanceToPlayer()
    {
        _distanceToPlayer = _player != null
            ? Vector2.Distance(transform.position, _player.position)
            : float.MaxValue;
    }

    protected Vector2 GetDirectionToPlayer()
    {
        if(_player == null) return Vector2.zero;
        Vector2 direction = (_player.position - transform.position).normalized;
        return direction;
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

    public void SetStoppingDistance(float distance)
    {
        _stoppingDistance = distance;
    }
}