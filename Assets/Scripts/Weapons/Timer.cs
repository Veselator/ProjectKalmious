using System;

public class Timer
{
    // Таймер для чего угодно

    // Bunch of important variables
    private float _tick;
    private float _time = 0;
    private float _targetTime = 0;

    private bool _isRunning = false;
    public bool IsRunning => _isRunning;

    public float Progress => _time / _targetTime;

    public event Action<float> OnTick;
    public event Action OnStart;
    public event Action OnEnd;

    public Timer(float tick = 0.1f)
    {
        _tick = tick;
    }

    public void Start(float target)
    {
        _time = 0;
        _targetTime = target;
        _isRunning = true;

        OnStart?.Invoke();
    }

    public void Tick()
    {
        if (!_isRunning) return;

        _time += _tick;
        OnTick?.Invoke(Progress);

        if (_time >= _targetTime)
        {
            _isRunning = false;
            OnEnd?.Invoke();
        }
    }
}
