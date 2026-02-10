using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField] private Health _linkedHealth;

    private void Awake()
    {
        _linkedHealth.OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        _linkedHealth.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
