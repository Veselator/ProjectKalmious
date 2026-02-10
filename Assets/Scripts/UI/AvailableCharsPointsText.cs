using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AvailableCharsPointsText : MonoBehaviour
{
    [SerializeField] private TMP_Text _linkedText;
    [SerializeField] private CharacteristicsHandler _chars;

    private void Start()
    {
        _chars.OnPointsChanged += HandlePointsChanged;
        HandlePointsChanged(_chars.PointsToAdd);
    }

    private void OnDestroy()
    {
        _chars.OnPointsChanged -= HandlePointsChanged;
    }

    private void HandlePointsChanged(int points)
    {
        _linkedText.text = $"Доступно балів: {points}";
    }
}
