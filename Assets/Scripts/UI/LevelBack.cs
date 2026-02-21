using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelBack : MonoBehaviour
{
    [SerializeField] private FixedPointsCameraTracker _camera;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _camera.SetTarget(0);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}