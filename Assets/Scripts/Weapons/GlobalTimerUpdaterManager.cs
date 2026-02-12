using System.Collections.Generic;
using UnityEngine;

public class GlobalTimerUpdaterManager : MonoBehaviour
{
    // Глобальный менеджер обновления таймеров

    private static List<Timer> _timers = new();

    public static void AddTimer(Timer timer)
    {
        _timers.Add(timer);
        Debug.Log($"Timer added. The length is {_timers.Count}");
    }

    public static void RemoveTimer(Timer timer)
    {
        _timers.Remove(timer);
        Debug.Log($"Timer removed. The length is {_timers.Count}");
    }

    private void Update()
    {
        if (_timers.Count == 0) return;

        foreach (var timer in _timers)
        {
            if (timer.IsRunning) timer.Tick(Time.deltaTime);
        }
    }
}
