using UnityEngine;
using System;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    // Базовый класс оружия
    // Инкапсулирует важные свойства и методы
    // Реализует полиморфизм и наследование

    [SerializeField] protected Damage _damage;
    [SerializeField] protected float _cooldown = 0.5f;
    public Timer CooldownTimer { get; private set; } = new Timer();

    public Damage DealedDamage => _damage;

    public event Action OnActStarted;
    public event Action OnActCompleted;

    private void Update()
    {
        if (CooldownTimer.IsRunning) CooldownTimer.Tick(Time.deltaTime);
    }

    public abstract void Act();

    public virtual bool CanAct()
    {
        return !CooldownTimer.IsRunning;
    }

    protected void StartAct()
    {
        CooldownTimer.Start(_cooldown);
        OnActStarted?.Invoke();
    }

    protected void CompleteAct()
    {
        OnActCompleted?.Invoke();
    }

    public void SetOverallDamageModifier(float modifier)
    {
        _damage.overallDamageMultiplier = modifier;
    }

    public void SetCriticalChance(float chance)
    {
        _damage.criticalChance = chance;
    }
}