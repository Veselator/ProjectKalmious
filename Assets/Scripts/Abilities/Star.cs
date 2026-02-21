using UnityEngine;

public class Star : BaseAbility
{
    // Сделал хитро - вместо создания новой системы для одной способности, просто
    // добавил невидимое оружие, которое стреляет звёздами по разным направлениям

    // TODO: никогда не называть класс Star так как при автозаполнении вызывает первее, чем Start
    // Ах, и ещё - система префиксов из исходников второй халвы вполне годная, её НАДО использовать!!!!!!!!

    [SerializeField] private BaseWeapon _starWeapon;
    protected override void Act()
    {
        _starWeapon.Act();
    }
}
