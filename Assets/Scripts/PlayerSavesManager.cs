using System;
using UnityEngine;

public class PlayerSavesManager : MonoBehaviour
{
    public static PlayerSavesManager Instance { get; private set; }

    private const int SlotCount = 3;
    private const string SlotKeyPrefix = "PlayerSlot_";

    public event Action<int, PlayerData> OnSaveSelected;
    public event Action<int, PlayerData> OnDataChanged;
    public event Action<int, PlayerData> OnNewSlotCreated;
    public event Action<int> OnSlotDeleted;

    private PlayerData[] _slots = new PlayerData[SlotCount];
    private int _currentSlotIndex = -1;

    public int CurrentSlotIndex => _currentSlotIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadAllSlots();
    }

    public bool IsSlotEmpty(int index)
    {
        ValidateIndex(index);
        return string.IsNullOrEmpty(_slots[index].Name);
    }

    public PlayerData GetSlotData(int index)
    {
        ValidateIndex(index);
        return _slots[index];
    }

    public PlayerData GetCurrentData()
    {
        if (_currentSlotIndex < 0)
            throw new InvalidOperationException("No slot selected.");
        return _slots[_currentSlotIndex];
    }

    public void SelectSlot(int index)
    {
        ValidateIndex(index);
        _currentSlotIndex = index;

        GameSetup.Instance.SetVisualChoice(_slots[index].VisualId);
        GameSetup.Instance.SetLevelId(_slots[index].LastSelectedLevelId);

        OnSaveSelected?.Invoke(index, _slots[index]);
    }

    public void CreateNewPlayer(string name)
    {
        if (_currentSlotIndex < 0)
            throw new InvalidOperationException("No slot selected for creation.");

        _slots[_currentSlotIndex] = PlayerData.CreateNew(name);
        SaveSlot(_currentSlotIndex);

        if (GameSetup.Instance != null)
        {
            GameSetup.Instance.SetVisualChoice(_slots[_currentSlotIndex].VisualId);
            GameSetup.Instance.SetLevelId(_slots[_currentSlotIndex].LastSelectedLevelId);
        }

        OnNewSlotCreated?.Invoke(_currentSlotIndex, _slots[_currentSlotIndex]);
    }

    public void UpdateVisualId(int visualId)
    {
        if (_currentSlotIndex < 0) return;

        PlayerData data = _slots[_currentSlotIndex];
        data.VisualId = visualId;
        _slots[_currentSlotIndex] = data;
        SaveSlot(_currentSlotIndex);

        if (GameSetup.Instance != null)
            GameSetup.Instance.SetVisualChoice(visualId);

        OnDataChanged?.Invoke(_currentSlotIndex, _slots[_currentSlotIndex]);
    }

    public void UpdateLastSelectedLevel(int levelId)
    {
        if (_currentSlotIndex < 0) return;

        PlayerData data = _slots[_currentSlotIndex];
        data.LastSelectedLevelId = levelId;
        _slots[_currentSlotIndex] = data;
        SaveSlot(_currentSlotIndex);

        if (GameSetup.Instance != null)
            GameSetup.Instance.SetLevelId(levelId);

        OnDataChanged?.Invoke(_currentSlotIndex, _slots[_currentSlotIndex]);
    }

    public void UpdateMaxLevelForMap(int mapIndex, int maxLevel)
    {
        if (_currentSlotIndex < 0) return;

        PlayerData data = _slots[_currentSlotIndex];
        if (data.MaxLevelsPerMap[mapIndex] > maxLevel) return;
        data.MaxLevelsPerMap[mapIndex] = maxLevel;
        _slots[_currentSlotIndex] = data;
        SaveSlot(_currentSlotIndex);

        OnDataChanged?.Invoke(_currentSlotIndex, _slots[_currentSlotIndex]);
    }

    public void DeleteSlot(int index)
    {
        ValidateIndex(index);

        _slots[index] = new PlayerData();
        PlayerPrefs.DeleteKey(GetSlotKey(index));
        PlayerPrefs.Save();

        if (_currentSlotIndex == index)
            _currentSlotIndex = -1;

        OnSlotDeleted?.Invoke(index);
    }

    private void LoadAllSlots()
    {
        for (int i = 0; i < SlotCount; i++)
            _slots[i] = LoadSlot(i);
    }

    private PlayerData LoadSlot(int index)
    {
        string key = GetSlotKey(index);
        if (!PlayerPrefs.HasKey(key))
            return new PlayerData();

        string json = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<PlayerData>(json);
    }

    private void SaveSlot(int index)
    {
        string json = JsonUtility.ToJson(_slots[index]);
        PlayerPrefs.SetString(GetSlotKey(index), json);
        PlayerPrefs.Save();
    }

    private string GetSlotKey(int index)
    {
        return SlotKeyPrefix + index;
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= SlotCount)
            throw new ArgumentOutOfRangeException(nameof(index), $"Slot index must be 0-{SlotCount - 1}.");
    }
}

[Serializable]
public struct PlayerData
{
    public string Name;
    public int VisualId;
    public int LastSelectedLevelId;
    public int[] MaxLevelsPerMap;

    public static PlayerData CreateNew(string name)
    {
        return new PlayerData
        {
            Name = name,
            VisualId = 0,
            LastSelectedLevelId = 0,
            MaxLevelsPerMap = new int[] { -1, -1, -1 }
        };
    }

    public bool IsEmpty => string.IsNullOrEmpty(Name);

    public int GetMaxLevelForMap(int mapIndex)
    {
        if (MaxLevelsPerMap == null || mapIndex < 0 || mapIndex >= MaxLevelsPerMap.Length)
            return -1;
        return MaxLevelsPerMap[mapIndex];
    }
}