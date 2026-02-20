using UnityEngine;
using System;

public class GlobalFlags : MonoBehaviour
{
    public static GlobalFlags Instance { get; private set; }

    public event Action<float> OnXpAdded;
<<<<<<< HEAD
    public event Action<BaseAI, Vector3> OnEnemySpawned;
    public event Action<float, Transform> OnDamage;
    public event Action<BaseAI, Vector3> OnEnemyKilled;
    public event Action OnLevelUp;
    public event Action OnGameOver;
=======
>>>>>>> parent of 819ee77 (Game over + particles for abilities)

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
<<<<<<< HEAD

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

    public void TriggerEnemySpawned(BaseAI enemy, Vector3 spawnPosition)
    {
        if (enemy == null || spawnPosition == null) return;
        OnEnemySpawned?.Invoke(enemy, spawnPosition);
    }

    public void TriggerLevelUp()
    {
        OnLevelUp?.Invoke();
    }

    public void TriggerGameOver()
    {
        OnGameOver?.Invoke();
    }
=======
>>>>>>> parent of 819ee77 (Game over + particles for abilities)
}