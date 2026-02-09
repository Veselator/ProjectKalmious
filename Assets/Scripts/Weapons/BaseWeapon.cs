using UnityEngine;
using System;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    // Базовый класс оружия
    // Инкапсулирует важные свойства и методы

    [SerializeField] protected Damage _damage;
    [SerializeField] protected float _cooldown = 0.5f;

    protected float _lastActTime;

    public Damage DealedDamage => _damage;

    public event Action OnActStarted;
    public event Action OnActCompleted;
    public event Action OnCooldownStarted;

    public abstract void Act();

    public virtual bool CanAct()
    {
        return Time.time >= _lastActTime + _cooldown;
    }

    protected void StartAct()
    {
        _lastActTime = Time.time;
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