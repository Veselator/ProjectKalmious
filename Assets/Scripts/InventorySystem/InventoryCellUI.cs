using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCellUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _selectedIndicator;

    private PlayerInventory _inventory;
    private WeaponInventoryItemSO _currentWeapon;
    public WeaponInventoryItemSO CurrentWeapon => _currentWeapon;
    private bool HasItem => _currentWeapon != null;

    private int _id;
    public int Id => _id;

    public event Action<int, PlayerInventory> OnItemInited;

    public void HandleSlotChange(int id) // CHANGE!
    {
        _selectedIndicator.SetActive(id == _id);
    }

    private void HandleItemChange(WeaponInventoryItemSO weapon, int id, bool _)
    {
        if (id != _id) return;

        _currentWeapon = weapon;
        UpdateWeaponIcon();
    }

    private void UpdateWeaponIcon()
    {
        if (HasItem)
        {
            _iconImage.gameObject.SetActive(true);
            _iconImage.sprite = _currentWeapon.Icon;
        }
        else
        {
            _iconImage.gameObject.SetActive(false);
        }
    }

    public void Initialize(int id, PlayerInventory inventory, WeaponInventoryItemSO item = null)
    {
        _id = id;
        _inventory = inventory;

        _selectedIndicator.SetActive(_inventory.CurrentPointer == _id);

        _inventory.OnCurrentSlotChanged += HandleSlotChange;
        _inventory.OnItemAdded += HandleItemChange;

        _currentWeapon = item;
        UpdateWeaponIcon();

        OnItemInited?.Invoke(id, inventory);
    }
}