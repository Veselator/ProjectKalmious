using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SlotClickHandler : MonoBehaviour
{
    [SerializeField] private int _slotIndex;
    [SerializeField] private TMP_Text _slotLabel;
    [SerializeField] private GameObject _nameInputPanel;
    private PlayerSavesManager _savesManager;
    private Button _button;

    [SerializeField] private string _emptySlotText = "Пустой слот";

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        _savesManager = PlayerSavesManager.Instance;
    }

    private void OnEnable()
    {
        RefreshLabel();

        _savesManager.OnNewSlotCreated += HandleSlotChanged;
        _savesManager.OnSlotDeleted += HandleSlotDeleted;
    }

    private void OnDisable()
    {
        _savesManager.OnNewSlotCreated -= HandleSlotChanged;
        _savesManager.OnSlotDeleted -= HandleSlotDeleted;
    }

    private void OnClick()
    {
        if (_savesManager.IsSlotEmpty(_slotIndex))
        {
            _savesManager.SelectSlot(_slotIndex);

            if (_nameInputPanel != null)
                _nameInputPanel.SetActive(true);
        }
        else
        {
            _savesManager.SelectSlot(_slotIndex);
            // Переход на экран 2
        }
    }

    private void RefreshLabel()
    {
        if (_slotLabel == null || PlayerSavesManager.Instance == null) return;

        if (_savesManager.IsSlotEmpty(_slotIndex))
            _slotLabel.text = _emptySlotText;
        else
            _slotLabel.text = _savesManager.GetSlotData(_slotIndex).Name;
    }

    private void HandleSlotChanged(int index, PlayerData data)
    {
        if (index == _slotIndex)
            RefreshLabel();
    }

    private void HandleSlotDeleted(int index)
    {
        if (index == _slotIndex)
            RefreshLabel();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}
