using UnityEngine;

public class CharacteristicIncreaser : MonoBehaviour
{
    [SerializeField] private CharacteristicNumberLinker _linker;
    private CharacteristicsHandler _chars;
    private CharacteristicType _charType;

    private void Start()
    {
        _chars = _linker.Characteristics;
        _charType = _linker.Type;
    }

    public void AddPoint()
    {
        if (!_chars.ArePointsAvailable) return;

        _chars.AddPoints(_charType);
    }
}
