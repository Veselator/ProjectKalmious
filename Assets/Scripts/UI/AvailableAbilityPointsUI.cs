using TMPro;
using UnityEngine;

public class AvailableAbilityPointsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _linkedText;
    [SerializeField] private AvailableAbilitiesManager _abilities;

    private void Start()
    {
        _abilities.OnPointsChanged += HandlePointsChanged;
        HandlePointsChanged(_abilities.AvailablePoints);
    }

    private void OnDestroy()
    {
        _abilities.OnPointsChanged -= HandlePointsChanged;
    }

    private void HandlePointsChanged(int points)
    {
        _linkedText.text = $"Доступно балів здібностей: {points}";
    }
}
