using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExitToMenuOnEscPressed : MonoBehaviour
{
    [SerializeField] private InputActionReference _escape;

    private void Start()
    {
        _escape.action.performed += _ => HandleEscape();
        _escape.action.Enable();
    }

    private void HandleEscape()
    {
        _escape.action.Disable();
        GameSceneManager.Instance.LoadMenu();
    }
}
