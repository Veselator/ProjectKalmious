using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private GameObject _nameInputPanel;

    [SerializeField] private FixedPointsCameraTracker _camera;

    private void Awake()
    {
        _confirmButton.onClick.AddListener(OnConfirm);
    }

    private void OnEnable()
    {
        _inputField.text = string.Empty;
    }

    private void OnConfirm()
    {
        if (_inputField == null || PlayerSavesManager.Instance == null) return;

        string playerName = _inputField.text.Trim();
        if (string.IsNullOrEmpty(playerName)) return;

        PlayerSavesManager.Instance.CreateNewPlayer(playerName);

        _camera.SetTarget(1);

        _nameInputPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        _confirmButton.onClick.RemoveListener(OnConfirm);
    }
}
