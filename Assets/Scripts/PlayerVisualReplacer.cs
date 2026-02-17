using UnityEngine;

public class PlayerVisualReplacer : MonoBehaviour
{
    [SerializeField] private GameObject[] _visuals;

    private void Start()
    {
        int choice = GameSetup.Instance.VisualChoice;
        ApplyVisual(choice);
    }

    private void ApplyVisual(int index)
    {
        _visuals[index].SetActive(true);
    }
}