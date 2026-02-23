using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] private PlayerLevelHandler _level;
    private GlobalFlags _globalFlags;

    private void Start()
    {
        _globalFlags = GlobalFlags.Instance;

        _globalFlags.OnGameOver += CheckEnd;
    }

    private void OnDestroy()
    {
        _globalFlags.OnGameOver -= CheckEnd;
    }

    private void CheckEnd()
    {
        PlayerSavesManager.Instance.UpdateMaxLevelForMap(GameSetup.Instance.LevelId, _level.CurrentLevel);
    }
}
