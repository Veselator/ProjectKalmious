using UnityEngine;

public class SimpleMenuShowAnimation : MonoBehaviour, IMenuShowAnimation
{
    private void Start()
    {
        Play(false);
    }

    public void Play(bool IsShowingAnim)
    {
        gameObject.SetActive(IsShowingAnim);
    }
}
