using UnityEngine;
using System;

public class Health : MonoBehaviour, IHealth
{
    // Здовоье может быть только у:
    // Игрока
    // Объекта врагов

    // Реализация для ShootemUp
    // Которая подойдёт и для платформера
    // Да и в обще отличная реализация
    // Здоровья всем родным программиста, который это написал
    // И в обще он крутой

    [SerializeField] private float _maxHealth;
    public float MaximumHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField] private float _maximumArmor;
    public float MaximumArmor
    {
        get => _maximumArmor;
        set
        {
            _maximumArmor = value;
        }
    }
    private float _currentArmor;

    public float CurrentArmor
    {
        get => Mathf.Clamp(_currentArmor, 0f, _maximumArmor);
        set
        {
            _currentArmor = Math.Max(value, 0f);
            OnArmorChanged?.Invoke(_currentArmor);
        }
    }

    [SerializeField] private bool isArmored;
    public bool DoesHaveArmor => isArmored;
    private float _currentHealth;
    public float CurrentHealth
    {
        get => Mathf.Clamp(_currentHealth, 0f, _maxHealth);
        set
        {
            _currentHealth = Math.Max(value, 0f);
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

    public bool IsDied => CurrentHealth <= 0f;
    public bool IsDamaged => _currentHealth < _maxHealth;
    public float CurrentHealthInPercentage => CurrentHealth / MaximumHealth;
    public float CurrentArmorInPercentage => CurrentArmor / MaximumArmor;

    public Action<float, Collider2D> OnDamaged { get; set; }
    public Action<float> OnCriticalHit { get; set; }
    public Action OnArmorDestoyed { get; set; }
    public Action OnDeath { get; set;  }
    public Action<float> OnHealthChanged { get; set; }
    public Action<float> OnArmorChanged { get; set; }

    public void Awake()
    {
        _currentHealth = MaximumHealth;
        _currentArmor = MaximumArmor;
    }

    public void Reset()
    {
        CurrentHealth = MaximumHealth;
        CurrentArmor = MaximumArmor;
    }

    public void ResetTo(float value)
    {
        MaximumHealth = value;
        CurrentHealth = value;
    }

    public void TakeDamage(Damage damage, Collider2D source)
    {
        // И так намучался - зачем после смерти добивать?
        if (IsDied) return; 

        float currentDamage = isArmored ? damage.MultipliedArmorDamage : damage.MultipliedHealthDamage;
        if (UnityEngine.Random.Range(0f, 100f) < damage.criticalChance)
        {
            currentDamage *= 2f;
            OnCriticalHit?.Invoke(currentDamage);
        }

        if (isArmored)
        {
            // Дополнительный урон
            _currentArmor -= currentDamage;

            if (_currentArmor <= 0f)
            {
                OnArmorDestoyed?.Invoke();
                isArmored = false;

                TakeDamage(damage, source);
            }
        }
        else
        {
            CurrentHealth -= currentDamage;
        }

        OnDamaged?.Invoke(currentDamage, source);

        if (IsDied) OnDeath?.Invoke();
    }
}