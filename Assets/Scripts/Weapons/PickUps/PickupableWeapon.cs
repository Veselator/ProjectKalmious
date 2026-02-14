using UnityEngine;

public class PickupableWeapon : MonoBehaviour, IPickupableWeapon
{
    // Подбираемое оружие
    // Нарушение SRP - отвечает и за графику, и за внутреннюю логику подбора
    private WeaponInventoryItemSO _data;
    private PlayerInventory _inventory;

    [SerializeField] private SpriteRenderer _topIcon;
    private bool _isItemAdded = false;

    public void Initialize(WeaponInventoryItemSO weaponData, PlayerInventory inventory)
    {
        _data = weaponData;
        _inventory = inventory;

        _topIcon.sprite = _data.Icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isItemAdded || !collision.TryGetComponent<PlayerInputHandler>(out _)) return;

        _isItemAdded = true;
        _inventory.AddItem(_data);
        Destroy(gameObject);
    }
}
