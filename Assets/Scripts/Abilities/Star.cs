using UnityEngine;

public class Star : BaseAbility
{
    // Сделал хитро - вместо создания новой системы для одной способности, просто
    // добавил невидимое оружие, которое стреляет звёздами по разным направлениям
    [SerializeField] private BaseWeapon _starWeapon;
    protected override void Act()
    {
        _starWeapon.Act();
    }
}
