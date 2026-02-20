using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject _inactiveVisual;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _unactiveText;
    [SerializeField] private Button _button;
    [SerializeField] private LevelDataSO _levelDataSO;

    [SerializeField] private string _lockedFormat = "Нужен ур. {0} на карте {1}";

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

        _unactiveText.gameObject.SetActive(!unlocked);
        if (!unlocked)
            _unactiveText.text = string.Format(_lockedFormat, _levelDataSO.RequiredMaxLevel, _levelDataSO.RequiredMapId + 1);
    }

    private void OnClick()
    {
        if (_levelDataSO == null) return;
        _savesManager.UpdateLastSelectedLevel(_levelDataSO.LevelId);
    }
}