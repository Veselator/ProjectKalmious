using UnityEngine;
using UnityEngine.UI;

public class PlayerVisualMenuHandler : MonoBehaviour
{
    [SerializeField] private Image _characterImage;
    [SerializeField] private PlayerVisualsSO _visualsSO;

    private PlayerSavesManager _savesManager;

    private void Start()
    {
        _savesManager = PlayerSavesManager.Instance;
        _savesManager.OnSaveSelected += HandleSaveSelected;
        _savesManager.OnDataChanged += HandleDataChanged;
    }

    private void OnDestroy()
    {
        _savesManager.OnSaveSelected -= HandleSaveSelected;
        _savesManager.OnDataChanged -= HandleDataChanged;
    }

    private void HandleSaveSelected(int slotIndex, PlayerData data)
    {
        ApplyVisual(data.VisualId);
    }

    private void HandleDataChanged(int slotIndex, PlayerData data)
    {
        ApplyVisual(data.VisualId);
    }

    private void ApplyVisual(int visualId)
    {
        Sprite sprite = _visualsSO.GetVisual(visualId);
        if (sprite != null)
            _characterImage.sprite = sprite;
    }
}