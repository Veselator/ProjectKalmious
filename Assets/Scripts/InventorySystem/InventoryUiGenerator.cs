using UnityEngine;
using System.Collections.Generic;

public class InventoryUIGenerator : MonoBehaviour
{
    // Генерирует UI инвентарь
    // А ещё связывает инвентарь и отдельные ячейки
    // Нарушение SRP
    // Но...
    // 

    // Также проблема - нельзя добавить предметы кроме оружия
    // А что если и не надо?

    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _centerPoint;

    [SerializeField] private float _animationDelay = 0.1f;
    [SerializeField] private bool _playStartAnimation = true;

    private List<InventoryCellUI> _cells;
    private int _prevSelect = 0;

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
            cellUI.Initialize(i);
            _cells.Add(cellUI);

            WeaponInventoryItemSO item = _inventory.GetItem(i);
            if (item != null)
            {
                cellUI.SetItem(item);
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
        if (index < 0 || index >= _cells.Count) return;

        _cells[index].SetItem(item);
    }

    private void HandleItemRemoved(WeaponInventoryItemSO item, int index)
    {
        if (index < 0 || index >= _cells.Count) return;

        _cells[index].Clear();
    }

    private void UpdateSelection(int selectedIndex)
    {
        if (selectedIndex < 0 || selectedIndex >= _cells.Count) return;

        _cells[_prevSelect].SetSelected(false);
        _prevSelect = selectedIndex;
        _cells[selectedIndex].SetSelected(true);
    }
}