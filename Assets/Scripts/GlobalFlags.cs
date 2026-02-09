using UnityEngine;
using System;

public class GlobalFlags : MonoBehaviour
{
    public static GlobalFlags Instance { get; private set; }

    public event Action<float> OnXpAdded;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddXp(float xp)
    {
        if (xp <= 0f)
            return;

        OnXpAdded?.Invoke(xp);
    }
}