using UnityEngine;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{
    // Реализация паттерна Object pool

    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _initialSize = 20;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _initialSize; i++)
        {
            GameObject obj = Instantiate(_prefab, transform.position, Quaternion.identity);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        GameObject obj = _pool.Count > 0 ? _pool.Dequeue() : Instantiate(_prefab);
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

    private void OnDestroy()
    {
        while (_pool.Count > 0)
        {
            Destroy(_pool.Dequeue());
        }
    }
}