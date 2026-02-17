using System.Collections.Generic;
using UnityEngine;

public class DamageTextAnimationManager : MonoBehaviour
{
    // Менеджер анимации плашек урона
    // Да, нарушение SRP так как он отвечает и за пул объектов, и за инициализацию, и за установление позиции

    [SerializeField] private GameObject _textPrefab;
    [SerializeField] private int _initialPoolSize = 20;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(0f, 0.5f, 0f);
    [SerializeField] private Transform _damageCanvas;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _initialPoolSize; i++) _pool.Enqueue(CreateInstance());

        GlobalFlags.Instance.OnDamage += HandleDamage;
    }

    private void OnDestroy()
    {
        GlobalFlags.Instance.OnDamage -= HandleDamage;
    }

    private void HandleDamage(float damage, Transform damagedObject)
    {
        GameObject obj = Get();
        obj.transform.position = damagedObject.position + _spawnOffset;

        DynamicDamageText damageText = obj.GetComponent<DynamicDamageText>();
        damageText.Init(damage, this);
    }

    private GameObject Get()
    {
        GameObject obj;

        if (_pool.Count > 0)
        {
            obj = _pool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            obj = CreateInstance();
            obj.SetActive(true);
        }

        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

    private GameObject CreateInstance()
    {
        GameObject obj = Instantiate(_textPrefab, _damageCanvas);
        obj.SetActive(false);
        return obj;
    }
}