using UnityEngine;
using UnityEngine.UI;

public class InventoryCellUI : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _selectedIndicator;

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
        if(isSelected) Debug.Log($"{_id} is now selected");
        _selectedIndicator.SetActive(isSelected);
    }

    public void Clear()
    {
        _currentItem = null;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        bool hasItem = _currentItem != null;

        _iconImage.enabled = hasItem;
        if (hasItem)
        {
            _iconImage.sprite = _currentItem.Icon;
        }
    }

    public void Initialize(int id)
    {
        _id = id;
        Clear();
    }
}