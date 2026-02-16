using UnityEngine;
using UnityEngine.UI;

public class ReturnToMenuButtonBinding : MonoBehaviour
{
    [SerializeField] private Button _linkedButton;

    private void OnEnable()
    {
        _linkedButton.onClick.AddListener(GameSceneManager.Instance.LoadMenu);
    }

    private void OnDisable()
    {
        _linkedButton.onClick.RemoveListener(GameSceneManager.Instance.LoadMenu);
    }
}