using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int _maxSlots = 5;

    private List<WeaponInventoryItemSO> _items;
    private int _currentPointer;

    public int CurrentPointer
    {
        get => _currentPointer;
        private set
        {
            int prevPointer = _currentPointer;
            _currentPointer = Mathf.Clamp(value, 0, _maxSlots - 1);

            if(prevPointer != _currentPointer) OnCurrentSlotChanged?.Invoke(_currentPointer);
        }
    }

    public WeaponInventoryItemSO CurrentItem => _items.Count > 0 && _currentPointer < _items.Count ? _items[_currentPointer] : null;
    public int ItemCount => _items.Count;
    public int MaxSlots => _maxSlots;

    public event Action<WeaponInventoryItemSO, int> OnItemAdded;
    public event Action<WeaponInventoryItemSO, int> OnItemRemoved;
    public event Action<int> OnCurrentSlotChanged;
    public event Action<int> OnSlotUpdateForced;

    private void Awake()
    {
        _items = new List<WeaponInventoryItemSO>();
        _currentPointer = 0;
    }

    public bool AddItem(WeaponInventoryItemSO item)
    {
        if (item == null) return false;

        if (_items.Count >= _maxSlots)
        {
            return false;
        }

        _items.Add(item);
        int index = _items.Count - 1;
        OnItemAdded?.Invoke(item, index);

        if (_items.Count - 1 == _currentPointer)
        {
            OnSlotUpdateForced?.Invoke(_currentPointer);
        }

        return true;
    }

    public bool RemoveItem(int index)
    {
        if (index < 0 || index >= _items.Count)
        {
            return false;
        }

        WeaponInventoryItemSO removedItem = _items[index];
        _items.RemoveAt(index);
        OnItemRemoved?.Invoke(removedItem, index);
        return true;
    }

    public bool RemoveItem(WeaponInventoryItemSO item)
    {
        int index = _items.IndexOf(item);
        if (index == -1)
        {
            return false;
        }

        return RemoveItem(index);
    }

    public WeaponInventoryItemSO GetItem(int index)
    {
        if (index < 0 || index >= _items.Count)
        {
            return null;
        }

        return _items[index];
    }

    public void SelectNext()
    {
        CurrentPointer = (_currentPointer + 1) % _maxSlots;
    }

    public void SelectPrevious()
    {
        CurrentPointer = (_currentPointer - 1 + _maxSlots) % _maxSlots;
    }

    public void SelectSlot(int index)
    {
        if (index >= 0 && index < _maxSlots)
        {
            CurrentPointer = index;
        }
    }
}