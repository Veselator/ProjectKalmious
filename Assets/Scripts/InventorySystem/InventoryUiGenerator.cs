using UnityEngine;
using System.Collections.Generic;

public class InventoryUIGenerator : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _centerPoint;

    [SerializeField] private float _animationDelay = 0.1f;
    [SerializeField] private bool _playStartAnimation = true;

    private List<InventoryCellUI> _cells;

    private void Start()
    {
        _cells = new List<InventoryCellUI>();
        GenerateCells();
        SubscribeToInventory();
        UpdateSelection(_inventory.CurrentPointer);
    }

    private void OnDestroy()
    {
        UnsubscribeFromInventory();
    }

    private void GenerateCells()
    {
        int maxSlots = _inventory.MaxSlots;

        for (int i = 0; i < maxSlots; i++)
        {
            GameObject cellObject = Instantiate(_cellPrefab, _centerPoint);

            InventoryCellUI cellUI = cellObject.GetComponent<InventoryCellUI>();
            if (cellUI != null)
            {
                cellUI.Initialize(i);
                _cells.Add(cellUI);

                WeaponInventoryItemSO item = _inventory.GetItem(i);
                if (item != null)
                {
                    cellUI.SetItem(item);
                }
            }

            if (_playStartAnimation)
            {
                cellObject.GetComponent<InventoryCellStartAnimation>().Play(i * _animationDelay);
            }
        }
    }

    private void SubscribeToInventory()
    {
        _inventory.OnItemAdded += HandleItemAdded;
        _inventory.OnItemRemoved += HandleItemRemoved;
        _inventory.OnCurrentSlotChanged += UpdateSelection;
    }

    private void UnsubscribeFromInventory()
    {
        _inventory.OnItemAdded -= HandleItemAdded;
        _inventory.OnItemRemoved -= HandleItemRemoved;
        _inventory.OnCurrentSlotChanged -= UpdateSelection;
    }

    private void HandleItemAdded(WeaponInventoryItemSO item, int index)
    {
        if (index >= 0 && index < _cells.Count)
        {
            _cells[index].SetItem(item);
        }
    }

    private void HandleItemRemoved(WeaponInventoryItemSO item, int index)
    {
        if (index >= 0 && index < _cells.Count)
        {
            _cells[index].Clear();
        }
    }

    private void UpdateSelection(int selectedIndex)
    {
        Debug.Log("Updating selection...");
        for (int i = 0; i < _cells.Count; i++)
        {
            _cells[i].SetSelected(i == selectedIndex);
        }
    }
}