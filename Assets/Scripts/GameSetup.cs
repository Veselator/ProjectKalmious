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
    }

    public void SetLevelId(int id)
    {
        _levelId = id;
    }

    public void ResetToDefaults()
    {
        _visualChoice = _defaultVisualChoice;
        _levelId = _defaultLevelId;
    }
}