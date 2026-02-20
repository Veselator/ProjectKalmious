using TMPro;
using UnityEngine;

public class LevelSelectTitle : MonoBehaviour
{
    [SerializeField] private TMP_Text _mainTitle;
    private PlayerSavesManager _playerSavesManager;

    private void Start()
    {
        _playerSavesManager = PlayerSavesManager.Instance;

        _playerSavesManager.OnSaveSelected += Refresh;
    }

    private void OnDestroy()
    {
        _playerSavesManager.OnSaveSelected -= Refresh;
    }

    public void Refresh(int id, PlayerData data)
    {
        if (_mainTitle == null) return;
        if (_playerSavesManager.CurrentSlotIndex < 0) return;

        _mainTitle.text = data.Name;
    }
}
