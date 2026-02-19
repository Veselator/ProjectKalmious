using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelBack : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // Переход на экран 1
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveListener(OnClick);
    }
}
