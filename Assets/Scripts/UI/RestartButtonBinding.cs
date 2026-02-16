using UnityEngine;
using UnityEngine.UI;

public class RestartButtonBinding : MonoBehaviour
{
    [SerializeField] private Button _linkedButton;

    private void OnEnable()
    {
        _linkedButton.onClick.AddListener(GameSceneManager.Instance.RestartGame);
    }

    private void OnDisable()
    {
        _linkedButton.onClick.RemoveListener(GameSceneManager.Instance.RestartGame);
    }
}