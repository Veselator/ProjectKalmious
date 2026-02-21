using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelDataSO : ScriptableObject
{
    [SerializeField] private string _levelName;
    [SerializeField] private int _levelId;
    [SerializeField] private int _requiredMapId;
    [SerializeField] private int _requiredMaxLevel;
    [SerializeField] private Sprite _icon;

    public string LevelName => _levelName;
    public int LevelId => _levelId;
    public int RequiredMapId => _requiredMapId;
    public int RequiredMaxLevel => _requiredMaxLevel;
    public Sprite Icon => _icon;

    public bool IsUnlocked(PlayerData data)
    {
        if (_requiredMaxLevel <= 0)
            return true;

        int playerLevel = data.GetMaxLevelForMap(_requiredMapId);
        return playerLevel >= _requiredMaxLevel;
    }
}
