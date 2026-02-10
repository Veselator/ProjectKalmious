using UnityEngine;

public class PlayerStartWeapon : MonoBehaviour
{
    // Выдаёт игроку стартовое оружие

    [SerializeField] private string _weaponId;
    [SerializeField] private PlayerInventory _inventory;

    private void Start()
    {
        _inventory.AddItem(GlobalWeapons.Instance.GetWeaponItemByTag(_weaponId));
    }
}
