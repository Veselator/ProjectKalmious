using System.Collections.Generic;
using UnityEngine;

public class StrengthChangeHandler : BaseChangeHandler
{
    [SerializeField] private PlayerCurrentWeapon _playerCurWeapon;
    private List<IWeapon> _curWeapons;

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
            weapon.SetOverallDamageModifier(_characteristics.CalculatedStrength);
        }
    }
}
