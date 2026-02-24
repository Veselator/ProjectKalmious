using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GlobalVolumeReplacer : MonoBehaviour
{
    [SerializeField] private VolumeProfile[] _profiles;

    private void Start()
    {
        GetComponent<Volume>().profile = _profiles[GameSetup.Instance[PlayerChoiceType.LevelId]];
    }
}
