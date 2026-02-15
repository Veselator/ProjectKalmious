using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities/AbilitySO")]
public class AbilitySO : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _pointsToUnlock;

    public string ID => _id;
    public GameObject Prefab => _prefab;
    public Sprite Icon => _icon;
    public int PointsToUnlock => _pointsToUnlock;
}