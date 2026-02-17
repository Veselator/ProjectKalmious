using UnityEngine;
using System;

public class GlobalFlags : MonoBehaviour
{
    public static GlobalFlags Instance { get; private set; }

    public event Action<float> OnXpAdded;
    public event Action<float, Transform> OnDamage;
    public event Action<BaseAI, Vector3> OnEnemyKilled;
    public event Action OnGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void TriggerAddXp(float xp)
    {
        if (xp <= 0f)
            return;

        OnXpAdded?.Invoke(xp);
    }

    public void TriggerTakeDamage(float damage, Transform damagedObject)
    {
        if (damage <= 0f) return;
        OnDamage?.Invoke(damage, damagedObject);
    }

    public void TriggerEnemyKilled(BaseAI enemy, Vector3 enemyPosition)
    {
        if (enemy == null || enemyPosition == null) return;
        OnEnemyKilled?.Invoke(enemy, enemyPosition);
    }

    public void TriggerGameOver()
    {
        OnGameOver?.Invoke();
    }
}