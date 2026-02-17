using UnityEngine;

public class CallGameOverOnDeath : MonoBehaviour
{
    private bool _isGameOver = false;
    [SerializeField] private Health _health;

    private void Start()
    {
        _health.OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        _health.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        if (_isGameOver) return;
        _isGameOver = true;
        GlobalFlags.Instance.TriggerGameOver();
    }
}
