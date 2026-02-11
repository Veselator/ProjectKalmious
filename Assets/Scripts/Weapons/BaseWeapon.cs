using UnityEngine;
using System;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    // Базовый класс оружия
    // Инкапсулирует важные свойства и методы
    // Реализует полиморфизм и наследование

    [SerializeField] protected Damage _damage;
    [SerializeField] protected float _cooldown = 0.5f;
    public Timer CooldownTimer { get; private set; }

    public Damage DealedDamage => _damage;

    public event Action OnActStarted;
    public event Action OnActCompleted;
    public event Action OnCooldownStarted;

    private void Awake()
    {
        CooldownTimer = new Timer(Time.deltaTime);
    }

    private void Update()
    {
        if (CooldownTimer.IsRunning) CooldownTimer.Tick();
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
        OnCooldownStarted?.Invoke();
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