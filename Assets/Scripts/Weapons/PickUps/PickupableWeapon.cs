using UnityEngine;

public class PickupableWeapon : MonoBehaviour, IPickupableWeapon
{
    // Подбираемое оружие

    private WeaponInventoryItemSO _data;
    private PlayerInventory _inventory;
    private PointersManager _pointersManager;

    [SerializeField] private SpriteRenderer _topIcon;
    private bool _isItemAdded = false;

    public void Initialize(WeaponInventoryItemSO weaponData, PlayerInventory inventory, PointersManager pm)
    {
        _data = weaponData;
        _inventory = inventory;
        _pointersManager = pm;

        _topIcon.sprite = _data.Icon;

        _pointersManager.TrackWeapon(transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isItemAdded || !collision.TryGetComponent<PlayerInputHandler>(out _)) return;

        _isItemAdded = true;
        _inventory.AddItem(_data);
        _pointersManager.UntrackWeapon(transform);
        Destroy(gameObject);
    }
}
