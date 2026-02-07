public class GlobalFlags
{
    // Фиксирует глобальные состояния системы - конец игры
    // Также работает как EventBus

    public static GlobalFlags Instance { get; private set; }

    public GlobalFlags()
    {
        Instance = this;
    }
}