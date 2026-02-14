using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private WaitForSeconds _spawnDelayCached;
    private WaitForSeconds _waveDelayCached;

    public WaveState CurrentWaveState => _currentWaveState;
    public int WaveId => _waveId;

    private List<EnemySettingsSO> _availableEnemiesCached = new List<EnemySettingsSO>();
    private float _totalWaveCached;

    public event Action<int> OnWaveStarted;
    public event Action<int> OnWaveEnded;

    private void Start()
    {
        _enemiesCMS = GlobalEnemies.Instance;

        _spawnDelayCached = new WaitForSeconds(_spawnInterval);
        _waveDelayCached = new WaitForSeconds(_timeBetweenWaves);

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
        float random = UnityEngine.Random.Range(0f, _totalWaveCached);
        float cumulative = 0f;

        foreach (var enemy in _availableEnemiesCached)
        {
            cumulative += 1 / (enemy.Difficulty * _maxEnemyDifficultyFactor);
            if (random <= cumulative)
                return enemy;
        }

        return _availableEnemiesCached[_availableEnemiesCached.Count - 1];
    }

    private void RecalculateCachedVariables()
    {
        _availableEnemiesCached.Clear();
        foreach (var e in _allEnemies)
        {
            if (e != null && e.Difficulty <= _waveId)
                _availableEnemiesCached.Add(e);
        }
        _totalWaveCached = 0f;

        foreach (var enemy in _availableEnemiesCached)
        {
            _totalWaveCached += 1f / (enemy.Difficulty * _maxEnemyDifficultyFactor);
        }
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        RecalculateCachedVariables();

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
            yield return _spawnDelayCached;
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
        yield return _waveDelayCached;
        StartNextWave();
    }
}

public enum WaveState
{
    WaveStarted,
    WaveOngoing,
    WaveEnded
}