using System.Collections.Generic;
using UnityEngine;

public class GlobalTimerUpdaterManager : MonoBehaviour
{
    // Глобальный менеджер обновления таймеров
    // Лучше было бы GlobalTimerUpdaterRegistry ибо Manager уже и так всюду и везде, из-за чего смысл теряется

    private static List<Timer> _timers = new();

    public static void AddTimer(Timer timer)
    {
        _timers.Add(timer);
    }

    public static void RemoveTimer(Timer timer)
    {
        _timers.Remove(timer);
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
