using System.Collections.Generic;
using UnityEngine;

public class StrengthChangeHandler : BaseChangeHandler
{
    [SerializeField] private PlayerCurrentWeapon _playerCurWeapon;
    private List<IWeapon> _curWeapons;

    protected override void Start()
    {
        base.Start();

        if (_playerCurWeapon.IsInited) Init();
        _playerCurWeapon.OnInited += Init;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _playerCurWeapon.OnInited -= Init;
    }

    private void Init()
    {
        _curWeapons = _playerCurWeapon.AllWeapons;
        DoChange();
    }

    protected override void DoChange()
    {
        if (_curWeapons == null) return;
        foreach (var weapon in _curWeapons)
        {
            weapon.SetOverallDamageModifier(_characteristics.CalculatedStrength);
        }
    }
}
