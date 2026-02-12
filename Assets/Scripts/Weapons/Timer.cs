using System;

public class Timer
{
    // Таймер для чего угодно

    // Bunch of important variables
    private float _time = 0f;
    private float _targetTime = 1f;

    private bool _isRunning = false;
    public bool IsRunning => _isRunning;

    public float Progress => _time / _targetTime;

    public event Action<float> OnTick;
    public event Action OnStart;
    public event Action OnEnd;

    public Timer()
    {
        GlobalTimerUpdaterManager.AddTimer(this);
    }

    public void Disable()
    {
        GlobalTimerUpdaterManager.RemoveTimer(this);
    }

    public void Start(float target)
    {
        _time = 0f;
        _targetTime = target;
        _isRunning = true;

        OnStart?.Invoke();
    }

    public void Tick(float tick)
    {
        if (!_isRunning) return;

        _time += tick;
        OnTick?.Invoke(Progress);

        if (_time >= _targetTime)
        {
            _isRunning = false;
            OnEnd?.Invoke();
        }
    }
}
