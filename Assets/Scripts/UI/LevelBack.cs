using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelBack : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // Переход на экран 1
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}