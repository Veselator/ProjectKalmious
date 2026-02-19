using System.Collections.Generic;
using UnityEngine;

public class PointersManager : MonoBehaviour
{
    [SerializeField] private GameObject _pointerPrefab;
    [SerializeField] private Transform _pointersParent;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private int _initialPoolSize = 20;
    [SerializeField] private float _orbitRadius = 200f;
    [SerializeField] private float _hideDistance = 8f;

    private Queue<PointerUI> _pool = new Queue<PointerUI>();
    private List<PointerUI> _activePointers = new List<PointerUI>();

    private void Start()
    {
        for (int i = 0; i < _initialPoolSize; i++) _pool.Enqueue(CreateInstance());

        GlobalFlags.Instance.OnEnemySpawned += HandleEnemySpawned;
        GlobalFlags.Instance.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDestroy()
    {
        GlobalFlags.Instance.OnEnemySpawned -= HandleEnemySpawned;
        GlobalFlags.Instance.OnEnemyKilled -= HandleEnemyKilled;
    }

    private void HandleEnemySpawned(BaseAI enemy, Vector3 position)
    {
        PointerUI pointer = Get();
        pointer.Initialize(enemy, _playerTransform, _orbitRadius, _hideDistance);
        _activePointers.Add(pointer);
    }

    private void HandleEnemyKilled(BaseAI enemy, Vector3 position)
    {
        for (int i = _activePointers.Count - 1; i >= 0; i--)
        {
            if (_activePointers[i].Target == enemy.transform)
            {
                Return(_activePointers[i]);
                _activePointers.RemoveAt(i);
                return;
            }
        }
    }

    public void TrackWeapon(Transform weaponTransform)
    {
        PointerUI pointer = Get();
        pointer.Initialize(weaponTransform, _playerTransform, _orbitRadius, _hideDistance);
        _activePointers.Add(pointer);
    }

    public void UntrackWeapon(Transform weaponTransform)
    {
        for (int i = _activePointers.Count - 1; i >= 0; i--)
        {
            if (_activePointers[i].Target == weaponTransform)
            {
                Return(_activePointers[i]);
                _activePointers.RemoveAt(i);
                return;
            }
        }
    }

    private void Update()
    {
        for (int i = _activePointers.Count - 1; i >= 0; i--)
        {
            if (_activePointers[i].Target == null)
            {
                Return(_activePointers[i]);
                _activePointers.RemoveAt(i);
            }
        }
    }

    private PointerUI Get()
    {
        if (_pool.Count > 0)
        {
            return _pool.Dequeue();
        }
        return CreateInstance();
    }

    private void Return(PointerUI pointer)
    {
        pointer.Deactivate();
        _pool.Enqueue(pointer);
    }

    private PointerUI CreateInstance()
    {
        GameObject obj = Instantiate(_pointerPrefab, _pointersParent);
        obj.SetActive(false);
        PointerUI pointer = obj.GetComponent<PointerUI>();
        return pointer;
    }
}