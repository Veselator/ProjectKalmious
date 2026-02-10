using UnityEngine;
using UnityEngine.UI;

public class InventoryCellUI : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _selectedIndicator;

    private WeaponInventoryItemSO _currentItem;
    private bool HasItem => _currentItem != null;

    public int Id => _id;
    public WeaponInventoryItemSO CurrentItem => _currentItem;

    public void SetItem(WeaponInventoryItemSO item)
    {
        _currentItem = item;
        UpdateVisuals();
    }

    public void SetSelected(bool isSelected)
    {
        _selectedIndicator.SetActive(isSelected);
    }

    public void Clear()
    {
        _currentItem = null;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (HasItem)
        {
            _iconImage.gameObject.SetActive(true);
            _iconImage.sprite = _currentItem.Icon;
        }
        else
        {
            _iconImage.gameObject.SetActive(false);
        }
    }

    public void Initialize(int id)
    {
        _id = id;
        _selectedIndicator.SetActive(false);
        Clear();
    }
}