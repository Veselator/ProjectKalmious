using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    // Класс, который отображает выбор игрока пот отношению к конкретной игре

    public static GameSetup Instance { get; private set; }

    [SerializeField] private int _defaultVisualChoice = 0;
    [SerializeField] private int _defaultLevelId = 0;

    private int _visualChoice;
    private int _levelId;

    public int VisualChoice => _visualChoice;
    public int LevelId => _levelId;
    private Dictionary<PlayerChoiceType, int> _playerChoice = new();

    public int this[PlayerChoiceType type] => _playerChoice[type];

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        ResetToDefaults();
    }

    public void SetVisualChoice(int choice)
    {
        _visualChoice = choice;
        _playerChoice[PlayerChoiceType.PlayerVisual] = choice;
    }

    public void SetLevelId(int id)
    {
        _levelId = id;
        _playerChoice[PlayerChoiceType.LevelId] = id;
    }

    public int GetChoice(PlayerChoiceType type)
    {
        return _playerChoice[type];
    }

    public void ResetToDefaults()
    {
        _visualChoice = _defaultVisualChoice;
        _levelId = _defaultLevelId;

        _playerChoice.Add(PlayerChoiceType.PlayerVisual, _defaultVisualChoice);
        _playerChoice.Add(PlayerChoiceType.LevelId, _defaultLevelId);
    }
}

public enum PlayerChoiceType
{
    PlayerVisual,
    LevelId
}