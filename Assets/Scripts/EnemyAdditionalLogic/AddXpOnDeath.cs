using UnityEngine;

public class AddXpOnDeath : MonoBehaviour
{
    [SerializeField] private int _addXp = 2;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _xpPointPrefab;
    [SerializeField] private float _spawnRadius = 0.5f;

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
        for (int i = 0; i < _addXp; i++)
        {
            Vector2 offset = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPos = transform.position + (Vector3)offset;
            Instantiate(_xpPointPrefab, spawnPos, Quaternion.identity);
        }
    }
}