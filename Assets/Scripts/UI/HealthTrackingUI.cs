using UnityEngine;
using UnityEngine.UI;

public class HealthTrackingUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _progress;

    private void Start()
    {
        _health.OnHealthChanged += HandleChange;

        HandleChange(0f);
    }

    private void OnDestroy()
    {
        _health.OnHealthChanged -= HandleChange;
    }

    private void HandleChange(float _)
    {
        _progress.fillAmount = _health.CurrentHealthInPercentage;
    }
}
