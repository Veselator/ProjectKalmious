using UnityEngine;

public class AbilitiesManagerController : MonoBehaviour
{
    [SerializeField] private AbilitiesManager _abilitiesManager;

    private KeyCode[] _slotKeys = new KeyCode[]
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4
    };

    private void Update()
    {
        for (int i = 0; i < _slotKeys.Length; i++)
        {
            if (Input.GetKeyDown(_slotKeys[i]))
            {
                _abilitiesManager.UseAbility(i);
                break;
            }
        }
    }
}