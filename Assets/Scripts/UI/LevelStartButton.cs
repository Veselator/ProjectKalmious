using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelStartButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (GameSceneManager.Instance != null)
            GameSceneManager.Instance.LoadGame();
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveListener(OnClick);
    }
}
