using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGameObjectOnGameOver : MonoBehaviour
{
    private GlobalFlags _globalFlags;

    private void Start()
    {
        _globalFlags = GlobalFlags.Instance;
        _globalFlags.OnGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        if (_globalFlags != null)
        {
            _globalFlags.OnGameOver -= HandleGameOver;
        }
    }

    private void HandleGameOver()
    {
        gameObject.SetActive(false);
    }
}
