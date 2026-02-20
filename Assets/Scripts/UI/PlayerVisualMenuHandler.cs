using UnityEngine;
using UnityEngine.UI;

public class PlayerVisualMenuHandler : MonoBehaviour
{
    [SerializeField] private Image _characterImage;
    [SerializeField] private PlayerVisualsSO _visualsSO;

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

    public void Refresh()
    {
        if (_characterImage == null || _visualsSO == null) return;
        if (PlayerSavesManager.Instance == null || PlayerSavesManager.Instance.CurrentSlotIndex < 0) return;

        int visualId = PlayerSavesManager.Instance.GetCurrentData().VisualId;
        Sprite sprite = _visualsSO.GetVisual(visualId);

        if (sprite != null)
            _characterImage.sprite = sprite;
    }

    private void HandleDataChanged(int slotIndex, PlayerData data)
    {
        Refresh();
    }
}
