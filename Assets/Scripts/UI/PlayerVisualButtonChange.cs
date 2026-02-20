using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayerVisualButtonChange : MonoBehaviour
{
    [SerializeField] private int _direction = 1;
    [SerializeField] private PlayerVisualsSO _visualsSO;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (PlayerSavesManager.Instance == null || _visualsSO == null) return;
        if (PlayerSavesManager.Instance.CurrentSlotIndex < 0) return;

        int current = PlayerSavesManager.Instance.GetCurrentData().VisualId;
        int count = _visualsSO.Count;
        if (count <= 0) return;

        int next = (current + _direction % count + count) % count;
        PlayerSavesManager.Instance.UpdateVisualId(next);
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveListener(OnClick);
    }
}
