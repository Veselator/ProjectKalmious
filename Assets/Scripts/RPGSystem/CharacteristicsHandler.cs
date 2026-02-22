using UnityEngine;
using System;

public class CharacteristicsHandler : MonoBehaviour
{
    // Отвечает за учёт текущих характеристик игрока

    private const int _maxCharacteristicValue = 100;
    public int MaxChars => _maxCharacteristicValue;

    [SerializeField] private PlayerLevelHandler _levelHandler;

    [SerializeField] private int _pointsPerLevel = 2;

    private float _speed = 5; // Понятно
    private float _strength = 5; // Сила персонажа - влияет на наносимый  урон
    private float _stamina = 5; // Влияет на максимальное здоровье
    private float _intelligence = 5; // Влияет на количество набираемых очков
    private float _luck = 5; // Влияет на шанс критического урона

    private int _pointsToAdd = 0;

    public float Speed
    {
        get => _speed;
        private set
        {
            _speed = Mathf.Clamp(value, 0f, _maxCharacteristicValue);
        }
    }

    public float Strength
    {
        get => _strength;
        private set
        {
            _strength = Mathf.Clamp(value, 0f, _maxCharacteristicValue);
        }
    }

    public float Stamina
    {
        get => _stamina;
        private set
        {
            _stamina = Mathf.Clamp(value, 0, _maxCharacteristicValue);
        }
    }

    public float Intelligence
    {
        get => _intelligence;
        private set
        {
            _intelligence = Mathf.Clamp(value, 0, _maxCharacteristicValue);
        }
    }

    public float Luck
    {
        get => _luck;
        private set
        {
            _luck = Mathf.Clamp(value, 0, _maxCharacteristicValue);
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

    public bool ArePointsAvailable => PointsToAdd > 0;

    public event Action<CharacteristicType> OnCharacteristicsChanged;
    public event Action<int> OnPointsChanged;

    public float CalculatedSpeed => SpeedCalculator.GetCharacteristic(this);
    public float CalculatedStrength => StrengthCalculator.GetCharacteristic(this);
    public float CalculatedStamina => StaminaCalculator.GetCharacteristic(this);
    public float CalculatedIntelligence => IntelligenceCalculator.GetCharacteristic(this);
    public float CalculatedCritChance => LuckCalculator.GetCharacteristic(this);

    private void Start()
    {
        HandleLevelUp(0);
    }

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
        PointsToAdd += (int)(_pointsPerLevel + CalculatedIntelligence);
        OnPointsChanged?.Invoke(_pointsToAdd);
    }

    public float GetCharacteristicUncalculated(CharacteristicType type)
    {
        return type switch
        {
            CharacteristicType.Speed => Speed,
            CharacteristicType.Strength => Strength,
            CharacteristicType.Stamina => Stamina,
            CharacteristicType.Intelligence => Intelligence,
            CharacteristicType.Luck => Luck,
            _ => 0f
        };
    }

    public void AddPoints(CharacteristicType type, int num = 1)
    {
        if (_pointsToAdd - num < 0) return;

        switch (type)
        {
            case CharacteristicType.Speed:
                if (_speed + num > _maxCharacteristicValue) return;
                Speed += num;
                break;
            case CharacteristicType.Strength:
                if (_strength + num >= _maxCharacteristicValue) return;
                Strength += num;
                break;
            case CharacteristicType.Stamina:
                if (_stamina + num >= _maxCharacteristicValue) return;
                Stamina += num;
                break;
            case CharacteristicType.Intelligence:
                if (_intelligence + num >= _maxCharacteristicValue) return;
                Intelligence += num;
                break;
            case CharacteristicType.Luck:
                if (_luck + num >= _maxCharacteristicValue) return;
                Luck += num;
                break;
            default:
                return;
        }

        PointsToAdd -= num;
        OnCharacteristicsChanged?.Invoke(type);
    }
}