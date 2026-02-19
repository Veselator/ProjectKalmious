using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonExit : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GameSceneManager.Instance.QuitGame();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}
