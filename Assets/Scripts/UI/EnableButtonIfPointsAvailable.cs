using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableButtonIfPointsAvailable : MonoBehaviour
{
    // ¬ключает кнопку если есть очки

    [SerializeField] private CharacteristicNumberLinker _linker;
    private CharacteristicsHandler _chars;
    private Button _button;

    private void Start()
    {
        _chars = _linker.Characteristics;
        _chars.OnPointsChanged += HandlePointsChanged;
        _button = GetComponent<Button>();

        HandlePointsChanged(_chars.PointsToAdd);
    }

    private void OnDestroy()
    {
        if(_chars != null) _chars.OnPointsChanged -= HandlePointsChanged;
    }

    private void HandlePointsChanged(int points)
    {
        _button.interactable = points > 0;
    }
}
