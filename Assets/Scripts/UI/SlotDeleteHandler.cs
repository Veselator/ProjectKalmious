using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SlotDeleteHandler : MonoBehaviour
{
    [SerializeField] private int _slotIndex;
    private PlayerSavesManager _psm;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        _psm = PlayerSavesManager.Instance;
    }

    private void OnClick()
    {
        if (!_psm.IsSlotEmpty(_slotIndex)) _psm.DeleteSlot(_slotIndex);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}
