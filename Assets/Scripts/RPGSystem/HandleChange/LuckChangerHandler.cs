using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckChangerHandler : BaseChangeHandler
{
    [SerializeField] private PlayerCurrentWeapon _playerCurWeapon;
    private List<IWeapon> _curWeapons;

    protected override CharacteristicType _targetCharacteristic => CharacteristicType.Luck;

    protected override void Start()
    {
        base.Start();

        _curWeapons = _playerCurWeapon.AllWeapons;

        DoChange();
    }

    protected override void DoChange()
    {
        foreach (var weapon in _curWeapons)
        {
            weapon.SetCriticalChance(_characteristics.CalculatedCritChance);
        }
    }
}
