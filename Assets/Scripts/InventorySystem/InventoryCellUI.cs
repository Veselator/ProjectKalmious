using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _selectedIndicator;
    [SerializeField] private GameObject _emptyIndicator;

    private WeaponInventoryItemSO _currentItem;

    public int Id => _id;
    public WeaponInventoryItemSO CurrentItem => _currentItem;

    public void SetItem(WeaponInventoryItemSO item)
    {
        _currentItem = item;
        UpdateVisuals();
    }

    public void SetSelected(bool isSelected)
    {
        if (_selectedIndicator != null)
        {
            _selectedIndicator.SetActive(isSelected);
        }
    }

    public void Clear()
    {
        _currentItem = null;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        bool hasItem = _currentItem != null;

        if (_iconImage != null)
        {
            _iconImage.enabled = hasItem;
            if (hasItem)
            {
                _iconImage.sprite = _currentItem.Icon;
            }
        }

        if (_emptyIndicator != null)
        {
            _emptyIndicator.SetActive(!hasItem);
        }
    }

    public void Initialize(int id)
    {
        _id = id;
        Clear();
    }
}