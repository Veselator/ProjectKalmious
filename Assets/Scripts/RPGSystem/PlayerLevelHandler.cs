using UnityEngine;
using System;

public class PlayerLevelHandler : MonoBehaviour
{
    public static PlayerLevelHandler Instance { get; private set; }
    private GlobalFlags _globalFlags;

    public event Action<int> OnLevelChanged;
    public event Action<float, float> OnXPChanged;

    [SerializeField] private float _levelFactor = 2f;
    [SerializeField] private float _levelAdditionalValue = 10f;

    private int _currentLevel = 1;
    private float _currentXP = 0f;
    private float _currentXPGoal;

    public int CurrentLevel => _currentLevel;
    public float CurrentXP => _currentXP;
    public float CurrentXPGoal => _currentXPGoal;
    public float XPProgress => _currentXP / _currentXPGoal;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _currentXPGoal = CalculateXPGoal(_currentLevel);
    }

    private void Start()
    {
        _globalFlags = GlobalFlags.Instance;
        _globalFlags.OnXpAdded += AddXP;
    }

    private void OnDestroy()
    {
        _globalFlags.OnXpAdded -= AddXP;
    }

    private float CalculateXPGoal(int level)
    {
        if (level <= 1) return _levelAdditionalValue;
            
        return _levelFactor * level * Mathf.Log(level, 2f) + _levelAdditionalValue;
    }

    public void SetLevelFactor(float newLevelFactor)
    {
        _levelFactor = newLevelFactor;
        _currentXPGoal = CalculateXPGoal(_currentLevel);

        OnXPChanged?.Invoke(_currentXP, _currentXPGoal);
    }

    public void SetLevelAdditionalValue(float newLevelAdditionalValue)
    {
        _levelAdditionalValue = newLevelAdditionalValue;
        _currentXPGoal = CalculateXPGoal(_currentLevel);

        OnXPChanged?.Invoke(_currentXP, _currentXPGoal);
    }

    public void AddXP(float amount)
    {
        _currentXP += amount;

        while (_currentXP >= _currentXPGoal)
        {
            _currentXP -= _currentXPGoal;
            _currentLevel++;
            _currentXPGoal = CalculateXPGoal(_currentLevel);

            OnLevelChanged?.Invoke(_currentLevel);
            _globalFlags.TriggerLevelUp();
        }

        OnXPChanged?.Invoke(_currentXP, _currentXPGoal);
    }
}
