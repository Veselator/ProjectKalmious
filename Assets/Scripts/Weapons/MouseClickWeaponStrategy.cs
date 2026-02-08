using UnityEngine;

public class MouseClickWeaponStrategy : IWeaponInputStrategy
{
    public void HandleInput(PlayerCurrentWeapon weaponManager)
    {
        if (Input.GetMouseButton(0))
        {
            weaponManager.UseCurrentWeapon();
        }
    }
}