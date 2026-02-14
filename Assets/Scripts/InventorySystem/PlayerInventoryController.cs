using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private float _scrollSensitivity = 1f;

    private float _scrollAccumulator;

    private void Update()
    {
        HandleScrollInput();
        //HandleNumberKeyInput();
    }

    private void HandleScrollInput()
    {
        float scrollDelta = Input.mouseScrollDelta.y;

        if (Mathf.Abs(scrollDelta) > 0.01f)
        {
            _scrollAccumulator += scrollDelta;

            if (_scrollAccumulator >= _scrollSensitivity)
            {
                _inventory.SelectPrevious();
                _scrollAccumulator = 0f;
            }
            else if (_scrollAccumulator <= -_scrollSensitivity)
            {
                _inventory.SelectNext();
                _scrollAccumulator = 0f;
            }
        }
    }

    //private void HandleNumberKeyInput()
    //{
    //    for (int i = 0; i < _inventory.MaxSlots && i < 9; i++)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Alpha1 + i))
    //        {
    //            _inventory.SelectSlot(i);
    //        }
    //    }
    //}
}