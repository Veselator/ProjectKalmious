using UnityEngine;
using UnityEngine.UI;

public class HealthTrackingUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _progress;
    [SerializeField] private float _changeSpeed = 3f;

    private void Update()
    {
        _progress.fillAmount = Mathf.Lerp(_progress.fillAmount, _health.CurrentHealthInPercentage, _changeSpeed * Time.deltaTime);
    }
}
