using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGameOverOnDeath : MonoBehaviour
{
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
        GlobalFlags.Instance.TriggerGameOver();
    }
}
