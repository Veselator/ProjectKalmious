using UnityEngine;
using System.Collections.Generic;

public class InventoryUIGenerator : MonoBehaviour
{
    // Генерирует UI инвентарь

    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _centerPoint;

    [SerializeField] private float _animationDelay = 0.1f;
    [SerializeField] private bool _playStartAnimation = true;

    private List<InventoryCellUI> _cells;

    public void Init()
    {
        _cells = new List<InventoryCellUI>();
        GenerateCells();
    }

    private void GenerateCells()
    {
        int maxSlots = _inventory.MaxSlots;

        for (int i = 0; i < maxSlots; i++)
        {
            GameObject cellObject = Instantiate(_cellPrefab, _centerPoint);

            InventoryCellUI cellUI = cellObject.GetComponent<InventoryCellUI>();
            WeaponInventoryItemSO item = _inventory.GetItem(i);

            cellUI.Initialize(i, _inventory, item);
            _cells.Add(cellUI);

            if (_playStartAnimation)
            {
                cellObject.GetComponent<InventoryCellStartAnimation>().Play(i * _animationDelay);
            }
        }
    }

    //private void SubscribeToInventory()
    //{
    //    _inventory.OnItemAdded += HandleItemAdded;
    //    _inventory.OnItemRemoved += HandleItemRemoved;
    //    _inventory.OnCurrentSlotChanged += UpdateSelection;
    //}

    //private void UnsubscribeFromInventory()
    //{
    //    _inventory.OnItemAdded -= HandleItemAdded;
    //    _inventory.OnItemRemoved -= HandleItemRemoved;
    //    _inventory.OnCurrentSlotChanged -= UpdateSelection;
    //}

    //private void HandleItemAdded(WeaponInventoryItemSO item, int index)
    //{
    //    if (index < 0 || index >= _cells.Count) return;

    //    _cells[index].SetItem(item);
    //}

    //private void HandleItemRemoved(WeaponInventoryItemSO item, int index)
    //{
    //    if (index < 0 || index >= _cells.Count) return;

    //    _cells[index].Clear();
    //}

    //private void UpdateSelection(int selectedIndex)
    //{
    //    if (selectedIndex < 0 || selectedIndex >= _cells.Count) return;

    //    _cells[_prevSelect].SetSelected(false);
    //    _prevSelect = selectedIndex;
    //    _cells[selectedIndex].SetSelected(true);
    //}
}