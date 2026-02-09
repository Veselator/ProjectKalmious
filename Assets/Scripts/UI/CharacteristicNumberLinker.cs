using TMPro;
using UnityEngine;

public class CharacteristicNumberLinker : MonoBehaviour
{
    [SerializeField] private string _title;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _pointsText;
    [SerializeField] private CharacteristicsHandler _characteristics;
    [SerializeField] private CharacteristicType _targetType;

    public CharacteristicsHandler Characteristics => _characteristics;
    public CharacteristicType Type => _targetType;

    private void Start()
    {
        _characteristics.OnCharacteristicsChanged += UpdateText;
        _titleText.text = _title;
        UpdateText(_targetType);
    }

    private void OnDestroy()
    {
        _characteristics.OnCharacteristicsChanged -= UpdateText;
    }

    private void UpdateText(CharacteristicType type)
    {
        if (type != _targetType) return;

        _pointsText.text = _characteristics.GetCharacteristicUncalculated(_targetType).ToString();
    }
}
