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

    [SerializeField] private float maxHealth;
    public float MaximumHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
        }
    }

    [SerializeField] private float maximumArmor;
    public float MaximumArmor
    {
        get => maximumArmor;
        set
        {
            maximumArmor = value;
        }
    }
    private float currentArmor;

    public GameObject Instance => this.gameObject;

    public float CurrentArmor
    {
        get => Mathf.Clamp(currentArmor, 0f, maximumArmor);
        set
        {
            currentArmor = Math.Max(value, 0f);
            OnArmorChanged?.Invoke();
        }
    }

    [SerializeField] private bool isArmored;
    public bool DoesHaveArmor => isArmored;
    private float currentHealth;
    public float CurrentHealth
    {
        get => Mathf.Clamp(currentHealth, 0f, maxHealth);
        set
        {
            currentHealth = Math.Max(value, 0f);
            OnHealthChanged?.Invoke();
        }
    }

    public bool IsDied => CurrentHealth == 0f;
    public float CurrentHealthInPercentage => CurrentHealth / MaximumHealth;
    public float CurrentArmorInPercentage => CurrentArmor / MaximumArmor;

    public Action<float> OnDamaged { get; set; }
    public Action<float> OnCriticalHit { get; set; }
    public Action OnArmorDestoyed { get; set; }
    public Action OnDeath { get; set;  }
    public Action OnHealthChanged { get; set; }
    public Action OnArmorChanged { get; set; }

    public void Awake()
    {
        currentHealth = MaximumHealth;
        currentArmor = MaximumArmor;
    }

    public void ResetHealth()
    {
        CurrentHealth = MaximumHealth;
        CurrentArmor = MaximumArmor;
    }

    public void TakeDamage(Damage damage)
    {
        float currentDamage = isArmored ? damage.MultipliedArmorDamage : damage.MultipliedHealthDamage;
        if (UnityEngine.Random.Range(0f, 100f) < damage.criticalChance)
        {
            currentDamage *= 2f;
            OnCriticalHit?.Invoke(currentDamage);
        }

        if (isArmored)
        {
            // Дополнительный урок
            currentArmor -= currentDamage;

            if (currentArmor <= 0f)
            {
                OnArmorDestoyed?.Invoke();
                isArmored = false;

                TakeDamage(damage);
            }
        }
        else
        {
            CurrentHealth -= currentDamage;
        }

        OnDamaged?.Invoke(currentDamage);

        if (IsDied) OnDeath?.Invoke();
    }
}