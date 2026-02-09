using UnityEngine;

public class ShowMenuOnButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject _menuToShow;
    private IMenuShowAnimation _menuShowAnimation;
    private bool _isMenuShown = false;

    private void Start()
    {
        _menuShowAnimation = _menuToShow.GetComponent<IMenuShowAnimation>();
    }

    public void ChangeShowness()
    {
        _isMenuShown = !_isMenuShown;

        if (_menuShowAnimation != null) _menuShowAnimation.Play(_isMenuShown);
    }
}
