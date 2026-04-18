using System.Collections.Generic;
using UnityEngine;

public class SetWaveCall : MonoBehaviour
{
    public int value;
    [Tooltip("If true the increases wave by value. If false sets wave to value")]
    public bool increment;

    public void Activate()
    {
        if (increment)
            GameManager.Instance.currentWave += value;
        else
            GameManager.Instance.currentWave = value;
    }
}
