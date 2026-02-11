using UnityEngine;

public class PlayerStartWeapon : MonoBehaviour
{
    // Выдаёт игроку стартовое оружие

    [SerializeField] private string[] _weapons;
    [SerializeField] private PlayerInventory _inventory;

    public void Init()
    {
        if (_weapons.Length == 0) return;

        foreach (var weaponId in _weapons)
        {
            if(!string.IsNullOrEmpty(weaponId))_inventory.AddItem(GlobalWeapons.Instance.GetWeaponItemByTag(weaponId));
        }
    }
}
