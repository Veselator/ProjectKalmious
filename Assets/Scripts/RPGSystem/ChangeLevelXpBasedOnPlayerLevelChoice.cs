using System;
using UnityEngine;

public class ChangeLevelXpBasedOnPlayerLevelChoice : MonoBehaviour
{
    [SerializeField] private PlayerLevelHandler _playerLevelHandler;
    [SerializeField] private LevellingSettings[] _levellingSettings;

    private void Start()
    {
        int playerChoice = GameSetup.Instance[PlayerChoiceType.LevelId];

        _playerLevelHandler.SetLevelFactor(_levellingSettings[playerChoice].LevelFactor);
        _playerLevelHandler.SetLevelAdditionalValue(_levellingSettings[playerChoice].LevelAdditionalValue);
    }
}

[Serializable]
public struct LevellingSettings
{
    public float LevelFactor;
    public float LevelAdditionalValue;
}