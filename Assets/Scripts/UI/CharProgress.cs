using UnityEngine;
using UnityEngine.UI;

public class CharProgress : MonoBehaviour
{
    [SerializeField] private CharacteristicNumberLinker _char;
    private CharacteristicsHandler _characteristicsHandler;
    private CharacteristicType _type;

    [SerializeField] private Image _progressImage;

    private void Start()
    {
        _characteristicsHandler = _char.Characteristics;
        _type = _char.Type;
        _characteristicsHandler.OnCharacteristicsChanged += HandlePointsChanged;

        HandlePointsChanged(_type);
    }

    private void OnDestroy()
    {
        _characteristicsHandler.OnCharacteristicsChanged -= HandlePointsChanged;
    }

    private void HandlePointsChanged(CharacteristicType c)
    {
        if (c != _type) return;
        _progressImage.fillAmount = _characteristicsHandler.GetCharacteristicUncalculated(_type) / _characteristicsHandler.MaxChars;
    }
}
