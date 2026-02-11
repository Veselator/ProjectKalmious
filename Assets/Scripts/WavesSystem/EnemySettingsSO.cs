using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemySettings", menuName = "Enemies/EnemySettings")]
public class EnemySettingsSO : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private GameObject _prefab;

    public string ID => _id;
    public GameObject Prefab => _prefab;
}