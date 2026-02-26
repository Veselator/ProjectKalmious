using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject _inactiveVisual;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _unactiveText;
    [SerializeField] private TMP_Text _numberText;
    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _selectedVisual;

    [SerializeField] private LevelDataSO _levelDataSO;

    [SerializeField] private string _lockedFormat = "Потрібен макс. рівень {0} на мапі {1}";
    [SerializeField] private string _numberFormat = "Мапа {0}";

    private PlayerSavesManager _savesManager;

    private void Start()
    {
        _savesManager = PlayerSavesManager.Instance;
        _button.onClick.AddListener(OnClick);
        _savesManager.OnSaveSelected += HandleSaveSelected;
        _savesManager.OnDataChanged += HandleDataChanged;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
        _savesManager.OnSaveSelected -= HandleSaveSelected;
        _savesManager.OnDataChanged -= HandleDataChanged;
    }

    private void HandleSaveSelected(int slotIndex, PlayerData data)
    {
        ApplyState(data);
    }

    private void HandleDataChanged(int slotIndex, PlayerData data)
    {
        ApplyState(data);
    }

    private void ApplyState(PlayerData data)
    {
        if (_levelDataSO == null) return;

        bool unlocked = _levelDataSO.IsUnlocked(data);

        _titleText.text = _levelDataSO.LevelName;
        _button.interactable = unlocked;
        _inactiveVisual.SetActive(!unlocked);
        _iconImage.sprite = _levelDataSO.Icon;
        _numberText.text = string.Format(_numberFormat, _levelDataSO.LevelId + 1);

        _unactiveText.gameObject.SetActive(!unlocked);

        if (!unlocked) _unactiveText.text = string.Format(_lockedFormat, _levelDataSO.RequiredMaxLevel, _levelDataSO.RequiredMapId + 1);
        _selectedVisual.SetActive(data.LastSelectedLevelId == _levelDataSO.LevelId);
    }

    private void OnClick()
    {
        if (_levelDataSO == null) return;
        _savesManager.UpdateLastSelectedLevel(_levelDataSO.LevelId);
    }
}