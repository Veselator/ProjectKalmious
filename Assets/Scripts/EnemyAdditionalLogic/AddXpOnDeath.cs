using UnityEngine;

public class AddXpOnDeath : MonoBehaviour
{
    [SerializeField] private float _addXp = 2;
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
        GlobalFlags.Instance.AddXp(_addXp);
    }
}
