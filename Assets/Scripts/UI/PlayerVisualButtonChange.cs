using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayerVisualButtonChange : MonoBehaviour
{
    [SerializeField] private int _direction = 1;
    [SerializeField] private PlayerVisualsSO _visualsSO;

    private PlayerSavesManager _savesManager;
    private Button _button;

    private void Start()
    {
        _savesManager = PlayerSavesManager.Instance;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (_savesManager.CurrentSlotIndex < 0) return;

        int current = _savesManager.GetCurrentData().VisualId;
        int count = _visualsSO.Count;
        if (count <= 0) return;

        int next = (current + _direction % count + count) % count;
        _savesManager.UpdateVisualId(next);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}