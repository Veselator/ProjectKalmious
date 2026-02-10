using UnityEngine;

public class PlayerStartWeapon : MonoBehaviour
{
    // Выдаёт игроку стартовое оружие

    [SerializeField] private WeaponInventoryItemSO _startWeapon;
    [SerializeField] private PlayerInventory _inventory;

    private void Start()
    {
        _inventory.AddItem(_startWeapon);
    }
}
