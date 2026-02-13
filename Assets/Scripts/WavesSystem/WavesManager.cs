using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _spawnInterval = 0.3f;
    [SerializeField] private int _baseEnemiesPerWave = 3;
    [SerializeField] private float _enemiesScaleFactor = 1.5f;
    [SerializeField] private float _maxEnemyDifficultyFactor = 20f;

    private GlobalEnemies _enemiesCMS;
    private WaveState _currentWaveState;
    private int _waveId = 0;
    private List<GameObject> _aliveEnemies = new List<GameObject>();
    private EnemySettingsSO[] _allEnemies;

    private WaitForSeconds _spawnDelayCashed;
    private WaitForSeconds _waveDelayCashed;

    public WaveState CurrentWaveState => _currentWaveState;
    public int WaveId => _waveId;

    public event Action<int> OnWaveStarted;
    public event Action<int> OnWaveEnded;

    private void Start()
    {
        _enemiesCMS = GlobalEnemies.Instance;

        _spawnDelayCashed = new WaitForSeconds(_spawnInterval);
        _waveDelayCashed = new WaitForSeconds(_timeBetweenWaves);

        StartNextWave();
    }

    public void StartNextWave()
    {
        _waveId++;
        _currentWaveState = WaveState.WaveStarted;
        OnWaveStarted?.Invoke(_waveId);
        _allEnemies = _enemiesCMS.GetAllEnemies();
        StartCoroutine(SpawnWaveCoroutine());
    }

    private EnemySettingsSO GetWeightedRandomEnemy()
    {
        var available = _allEnemies.Where(e => e != null && e.Difficulty <= _waveId).ToList();
        if (available.Count == 0)
            return null;

        float totalWeight = 0f;
        foreach (var enemy in available)
            totalWeight += 1 / (enemy.Difficulty * _maxEnemyDifficultyFactor);

        float random = UnityEngine.Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var enemy in available)
        {
            cumulative += 1 / (enemy.Difficulty * _maxEnemyDifficultyFactor);
            if (random <= cumulative)
                return enemy;
        }

        return available[available.Count - 1];
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        int enemyCount = Mathf.RoundToInt(_baseEnemiesPerWave * _waveId * _enemiesScaleFactor);
        if (_allEnemies == null || _allEnemies.Length == 0)
            yield break;

        _currentWaveState = WaveState.WaveOngoing;

        for (int i = 0; i < enemyCount; i++)
        {
            EnemySettingsSO settings = GetWeightedRandomEnemy();
            if (settings == null)
                continue;

            Transform spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
            GameObject enemy = Instantiate(settings.Prefab, spawnPoint.position, Quaternion.identity);

            BaseAI ai = enemy.GetComponent<BaseAI>();
            ai.Initialize(_playerTransform);

            Health health = enemy.GetComponent<Health>();
            health.OnDeath += () => OnEnemyDied(enemy);

            _aliveEnemies.Add(enemy);
            yield return _spawnDelayCashed;
        }
    }

    private void OnEnemyDied(GameObject enemy)
    {
        _aliveEnemies.Remove(enemy);
        if (_aliveEnemies.Count <= 0 && _currentWaveState == WaveState.WaveOngoing)
        {
            _currentWaveState = WaveState.WaveEnded;
            OnWaveEnded?.Invoke(_waveId);
            StartCoroutine(WaitAndStartNextWave());
        }
    }

    private IEnumerator WaitAndStartNextWave()
    {
        yield return _waveDelayCashed;
        StartNextWave();
    }
}

public enum WaveState
{
    WaveStarted,
    WaveOngoing,
    WaveEnded
}