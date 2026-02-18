using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthTextLinker : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _health.OnHealthChanged += UpdateHealthText;
    }

    private void OnDestroy()
    {
        _health.OnHealthChanged -= UpdateHealthText;
    }

    private void UpdateHealthText(float value)
    {
        _text.text = $"{(int)_health.CurrentHealth} / {(int)_health.MaximumHealth}";
    }
}
