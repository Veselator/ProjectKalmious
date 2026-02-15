using System;
using UnityEngine;

public abstract class BaseChangeHandler : MonoBehaviour
{
    // Базовый класс для иерархии классов, которые связывают "приказ из штаба" и "ситуацию на земле"
    // То, какие характеристики приходят от Handler 
    // и как они меняют конкретное значение

    [SerializeField] protected CharacteristicsHandler _characteristics;
    [SerializeField] private CharacteristicType[] _targetCharacteristic;

    protected virtual void Start()
    {
        _characteristics.OnCharacteristicsChanged += HandleChange;
    }

    protected virtual void OnDestroy()
    {
        _characteristics.OnCharacteristicsChanged -= HandleChange;
    }

    private bool IsMatch(CharacteristicType type)
    {
        foreach (var c in _targetCharacteristic)
        {
            if(c == type) return true;
        }

        return false;
    }

    private void HandleChange(CharacteristicType type)
    {
        if (IsMatch(type)) DoChange();
    }

    protected abstract void DoChange();
}