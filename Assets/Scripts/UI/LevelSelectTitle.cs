using TMPro;
using UnityEngine;

public class LevelSelectTitle : MonoBehaviour
{
    [SerializeField] private TMP_Text _mainTitle;

    private void OnEnable()
    {
        Refresh();

        if (PlayerSavesManager.Instance != null)
            PlayerSavesManager.Instance.OnDataChanged += HandleDataChanged;
    }

    private void OnDisable()
    {
        if (PlayerSavesManager.Instance != null)
            PlayerSavesManager.Instance.OnDataChanged -= HandleDataChanged;
    }

    public void Refresh()
    {
        if (_mainTitle == null) return;
        if (PlayerSavesManager.Instance == null || PlayerSavesManager.Instance.CurrentSlotIndex < 0) return;

        _mainTitle.text = PlayerSavesManager.Instance.GetCurrentData().Name;
    }

    private void HandleDataChanged(int slotIndex, PlayerData data)
    {
        Refresh();
    }
}
