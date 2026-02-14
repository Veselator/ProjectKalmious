using UnityEngine;
using UnityEngine.UI;

public class WeaponCooldownUI : MonoBehaviour
{
    [SerializeField] private InventoryCellUI _linkedCell;
    [SerializeField] private Image _linkedImage;

    private int _id;
    private PlayerInventory _inventory;
    private PlayerCurrentWeapon _curWeapon;
    private BaseWeapon _weapon;

    private void Awake()
    {
        _linkedImage.fillAmount = 0f;
        _linkedCell.OnItemInited += HandleItemInited;
    }

    private void OnDestroy()
    {
        _linkedCell.OnItemInited -= HandleItemInited;

        if (_inventory == null) return;
        _inventory.OnItemAdded -= HandleItemAdded;

        if (_weapon == null) return;

        _weapon.CooldownTimer.OnStart -= HandleProgressStart;
        _weapon.CooldownTimer.OnTick -= HandleProgressTick;
        _weapon.CooldownTimer.OnEnd -= HandleProgressEnd;
    }

    private void HandleItemInited(int id, PlayerInventory inventory)
    {
        _id = id;
        _inventory = inventory;
        _inventory.OnItemAdded += HandleItemAdded;
        _curWeapon = PlayerCurrentWeapon.Instance;

        if (_linkedCell.CurrentWeapon == null) return;

        // Жопа, по-нормальному надо переписывать
        _weapon = _curWeapon.GetWeaponObjectByTag(_linkedCell.CurrentWeapon).GetComponent<BaseWeapon>();
        SubscribeToWeaponTimerEvents();
    }

    private void HandleItemAdded(WeaponInventoryItemSO item, int id)
    {
        if (id != _id) return;

        if (_weapon != null)
        {
            _weapon.CooldownTimer.OnStart -= HandleProgressStart;
            _weapon.CooldownTimer.OnTick -= HandleProgressTick;
            _weapon.CooldownTimer.OnEnd -= HandleProgressEnd;
        }

        _weapon = _curWeapon.GetWeaponObjectByTag(item).GetComponent<BaseWeapon>();
        SubscribeToWeaponTimerEvents();
    }

    private void SubscribeToWeaponTimerEvents()
    {
        if (_weapon != null)
        {
            _weapon.CooldownTimer.OnStart += HandleProgressStart;
            _weapon.CooldownTimer.OnTick += HandleProgressTick;
            _weapon.CooldownTimer.OnEnd += HandleProgressEnd;
        }
    }

    private void HandleProgressStart()
    {
        _linkedImage.fillAmount = 0f;
    }

    private void HandleProgressTick(float progress)
    {
        _linkedImage.fillAmount = 1f - progress;
    }

    private void HandleProgressEnd()
    {
        _linkedImage.fillAmount = 0f;
    }
}
