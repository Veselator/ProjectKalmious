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

    private void OnEnable()
    {
        if (PlayerSavesManager.Instance != null)
            PlayerSavesManager.Instance.OnDataChanged += HandleDataChanged;

        Refresh();
    }

    private void OnDisable()
    {
        if (PlayerSavesManager.Instance != null)
            PlayerSavesManager.Instance.OnDataChanged -= HandleDataChanged;
    }

    private void Awake()
    {
        if (_button != null)
            _button.onClick.AddListener(OnClick);
    }

    public void Refresh()
    {
        if (_levelDataSO == null) return;
        if (PlayerSavesManager.Instance == null || PlayerSavesManager.Instance.CurrentSlotIndex < 0) return;

        PlayerData data = PlayerSavesManager.Instance.GetCurrentData();
        bool unlocked = _levelDataSO.IsUnlocked(data);

        if (_titleText != null)
            _titleText.text = _levelDataSO.LevelName;

        if (_button != null)
            _button.interactable = unlocked;

        if (_inactiveVisual != null)
            _inactiveVisual.SetActive(!unlocked);

        if (_unactiveText != null)
        {
            _unactiveText.gameObject.SetActive(!unlocked);
            if (!unlocked)
                _unactiveText.text = string.Format(_lockedFormat, _levelDataSO.RequiredMaxLevel, _levelDataSO.RequiredMapId + 1);
        }
    }

    private void OnClick()
    {
        if (_levelDataSO == null || PlayerSavesManager.Instance == null) return;

        PlayerSavesManager.Instance.UpdateLastSelectedLevel(_levelDataSO.LevelId);
    }

    private void HandleDataChanged(int slotIndex, PlayerData data)
    {
        Refresh();
    }

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnClick);
    }
}
