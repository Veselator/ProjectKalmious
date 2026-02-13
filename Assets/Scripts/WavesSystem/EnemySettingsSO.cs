using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemySettings", menuName = "Enemies/EnemySettings")]
public class EnemySettingsSO : ScriptableObject
{
    // Настройка конкретного врага

    [SerializeField] private string _id;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _difficulty;

    public string ID => _id;
    public GameObject Prefab => _prefab;
    public int Difficulty => _difficulty;
}