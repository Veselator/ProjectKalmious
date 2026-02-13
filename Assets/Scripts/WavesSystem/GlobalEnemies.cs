using UnityEngine;
using System.Collections.Generic;

public class GlobalEnemies : MonoBehaviour
{
    // Глобальный класс врагов, инкапсулирует все сущности врагов

    public static GlobalEnemies Instance { get; private set; }

    [SerializeField] private EnemiesConfigSO _config;

    private Dictionary<string, GameObject> _entries = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        foreach (var settings in _config.enemies)
        {
            if (settings != null && !_entries.ContainsKey(settings.ID))
                _entries.Add(settings.ID, settings.Prefab);
        }
    }

    public EnemySettingsSO[] GetAllEnemies() => _config.enemies;

    public GameObject GetEnemyById(string id)
    {
        _entries.TryGetValue(id, out var prefab);
        return prefab;
    }
}