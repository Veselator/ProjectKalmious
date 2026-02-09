using UnityEngine;

public abstract class BaseChangeHandler : MonoBehaviour
{
    // Базовый класс для иерархии классов, которые связывают "приказ из штаба" и "ситуацию на земле"
    // То, какие характеристики приходят от Handler 
    // и как они меняют конкретное значение

    [SerializeField] protected CharacteristicsHandler _characteristics;
    protected virtual CharacteristicType _targetCharacteristic { get; }

    protected virtual void Start()
    {
        _characteristics.OnCharacteristicsChanged += HandleChange;
    }

    private void OnDestroy()
    {
        _characteristics.OnCharacteristicsChanged -= HandleChange;
    }

    private void HandleChange(CharacteristicType type)
    {
        if (_targetCharacteristic == type) DoChange();
    }

    protected abstract void DoChange();
}
