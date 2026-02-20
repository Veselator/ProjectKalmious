using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelStartButton : MonoBehaviour
{
    private Button _button;
    private GameSceneManager _sceneManager;

    private void Start()
    {
        _button = GetComponent<Button>();
        _sceneManager = GameSceneManager.Instance;
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _sceneManager.LoadGame();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}