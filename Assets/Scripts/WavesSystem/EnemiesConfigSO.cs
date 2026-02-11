using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemiesConfig", menuName = "Enemies/EnemiesConfig")]
public class EnemiesConfigSO : ScriptableObject
{
    public EnemySettingsSO[] enemies;
}