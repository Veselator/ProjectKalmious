using System.Collections.Generic;
using UnityEngine;

public class SpawnParticlePool : MonoBehaviour
{
    [SerializeField] private Transform _particlesParent;
    [SerializeField] private GameObject[] _particlePrefabs;
    [SerializeField] private int _initialPoolSize = 10;

    private Queue<ParticleSystem> _pool = new Queue<ParticleSystem>();

    GameObject RandomParticleObject => _particlePrefabs[Random.Range(0, _particlePrefabs.Length)];

    private void Start()
    {
        for (int i = 0; i < _initialPoolSize; i++)
            _pool.Enqueue(CreateInstance());

        GlobalFlags.Instance.OnEnemySpawned += HandleEnemySpawned;
    }

    private void OnDestroy()
    {
        GlobalFlags.Instance.OnEnemySpawned -= HandleEnemySpawned;
    }

    private void HandleEnemySpawned(BaseAI enemy, Vector3 position)
    {
        ParticleSystem ps = Get();
        ps.transform.position = position;
        ps.Play();
    }

    private void Update()
    {
        ReturnFinished();
    }

    private void ReturnFinished()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (!child.gameObject.activeSelf) continue;

            ParticleSystem ps = child.GetComponent<ParticleSystem>();
            if (ps != null && !ps.isPlaying) Return(ps);
        }
    }

    private ParticleSystem Get()
    {
        ParticleSystem ps;

        if (_pool.Count > 0)
        {
            ps = _pool.Dequeue();
            ps.gameObject.SetActive(true);
        }
        else
        {
            ps = CreateInstance();
            ps.gameObject.SetActive(true);
        }

        return ps;
    }

    private void Return(ParticleSystem ps)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ps.gameObject.SetActive(false);
        _pool.Enqueue(ps);
    }

    private ParticleSystem CreateInstance()
    {
        GameObject obj = Instantiate(RandomParticleObject, _particlesParent);
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();

        var main = ps.main;
        main.playOnAwake = false;
        main.stopAction = ParticleSystemStopAction.None;

        obj.SetActive(false);
        return ps;
    }
}
