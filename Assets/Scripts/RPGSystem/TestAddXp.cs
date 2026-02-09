using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddXp : MonoBehaviour
{
    [SerializeField] private float AddXp;

    public void AddXP()
    {
        PlayerLevelHandler.Instance.AddXP(AddXp);
    }
}
