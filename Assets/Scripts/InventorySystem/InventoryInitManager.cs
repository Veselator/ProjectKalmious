using UnityEngine;

public class InventoryInitManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerStartWeapon _startWeapon;
    [SerializeField] private PlayerCurrentWeapon _currentWeapon;
    [SerializeField] private InventoryUIGenerator _inventoryUIGenerator;

    private void Start()
    {
        _inventory.Init();
        _startWeapon.Init();
        _currentWeapon.Init();
        _inventoryUIGenerator.Init();
    }
}
