using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsTimerUpdater : MonoBehaviour
{
    [SerializeField] private List<BaseWeapon> _weapons = new();

    public void AddWeapon(BaseWeapon weapon)
    {
        _weapons.Add(weapon);
    }

    private void Update()
    {
        if (_weapons.Count == 0) return;

        foreach (var weapon in _weapons)
        {
            Timer timer = weapon.CooldownTimer;
        }
    }
}
