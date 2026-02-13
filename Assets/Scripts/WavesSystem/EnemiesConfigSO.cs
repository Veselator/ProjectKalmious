using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemiesConfig", menuName = "Enemies/EnemiesConfig")]
public class EnemiesConfigSO : ScriptableObject
{
    // Конфиг всех врагов в игре
    public EnemySettingsSO[] enemies;
}