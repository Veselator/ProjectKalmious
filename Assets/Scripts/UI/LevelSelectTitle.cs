using TMPro;
using UnityEngine;

public class LevelSelectTitle : MonoBehaviour
{
    [SerializeField] private TMP_Text _mainTitle;

    private PlayerSavesManager _savesManager;

    private void Start()
    {
        _savesManager = PlayerSavesManager.Instance;
        _savesManager.OnSaveSelected += HandleSaveSelected;
    }

    private void OnDestroy()
    {
        _savesManager.OnSaveSelected -= HandleSaveSelected;
    }

    private void HandleSaveSelected(int slotIndex, PlayerData data)
    {
        _mainTitle.text = data.Name;
    }
}