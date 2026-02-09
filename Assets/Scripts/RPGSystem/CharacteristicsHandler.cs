using UnityEngine;
using System;

public class CharacteristicsHandler : MonoBehaviour
{
    // Отвечает за учёт текущих характеристик игрока

    private const int MaxCharacteristicValue = 100;

    [SerializeField] private PlayerLevelHandler _levelHandler;

    [SerializeField] private int _pointsPerLevel = 2;

    private float _speed = 5; // Понятно
    private float _strength = 5; // Сила персонажа - влияет на наносимый  урон
    private float _stamina = 5; // Влияет на максимальное здоровье
    private float _intelligence = 5; // Влияет на количество набираемых очков
    private float _luck = 5; // Влияет на шанс критического урона

    private int _pointsToAdd = 2;

    public float Speed
    {
        get => _speed;
        private set
        {
            _speed = Mathf.Clamp(value, 0f, MaxCharacteristicValue);
        }
    }

    public float Strength
    {
        get => _strength;
        private set
        {
            _strength = Mathf.Clamp(value, 0f, MaxCharacteristicValue);
        }
    }

    public float Stamina
    {
        get => _stamina;
        private set
        {
            _stamina = Mathf.Clamp(value, 0, MaxCharacteristicValue);
        }
    }

    public float Intelligence
    {
        get => _intelligence;
        private set
        {
            _intelligence = Mathf.Clamp(value, 0, MaxCharacteristicValue);
        }
    }

    public float Luck
    {
        get => _luck;
        private set
        {
            _luck = Mathf.Clamp(value, 0, MaxCharacteristicValue);
        }
    }

    public int PointsToAdd
    {
        get => _pointsToAdd;
        private set
        {
            _pointsToAdd = Mathf.Max(value, 0);
            OnPointsChanged?.Invoke(_pointsToAdd);
        }
    }

    public event Action<CharacteristicType> OnCharacteristicsChanged;
    public event Action<int> OnPointsChanged;

    public float CalculatedSpeed => SpeedCalculator.GetCharacteristic(this);
    public float CalculatedStrength => StrengthCalculator.GetCharacteristic(this);
    public float CalculatedStamina => StaminaCalculator.GetCharacteristic(this);
    public float CalculatedIntelligence => IntelligenceCalculator.GetCharacteristic(this);
    public float CalculatedCritChance => LuckCalculator.GetCharacteristic(this);

    private void OnEnable()
    {
        _levelHandler.OnLevelChanged += HandleLevelUp;
    }

    private void OnDisable()
    {
        _levelHandler.OnLevelChanged -= HandleLevelUp;
    }

    private void HandleLevelUp(int newLevel)
    {
        PointsToAdd += (int)(_pointsPerLevel + 2f * CalculatedIntelligence);
        OnPointsChanged?.Invoke(_pointsToAdd);
    }

    public void AddPoint(CharacteristicType type)
    {
        if (_pointsToAdd <= 0) return;

        switch (type)
        {
            case CharacteristicType.Speed:
                if (_speed >= MaxCharacteristicValue) return;
                Speed += 1;
                break;
            case CharacteristicType.Strength:
                if (_strength >= MaxCharacteristicValue) return;
                Strength += 1;
                break;
            case CharacteristicType.Stamina:
                if (_stamina >= MaxCharacteristicValue) return;
                Stamina += 1;
                break;
            case CharacteristicType.Intelligence:
                if (_intelligence >= MaxCharacteristicValue) return;
                Intelligence += 1;
                break;
            case CharacteristicType.Luck:
                if (_luck >= MaxCharacteristicValue) return;
                Luck += 1;
                break;
            default:
                return;
        }

        PointsToAdd -= 1;
        OnCharacteristicsChanged?.Invoke(type);
    }
}