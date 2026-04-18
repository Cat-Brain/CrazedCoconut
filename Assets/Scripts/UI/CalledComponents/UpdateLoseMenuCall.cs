using System;
using TMPro;
using UnityEngine;

public class UpdateLoseMenuCall : MonoBehaviour
{
    public TextMeshProUGUI waveText, timeText;
    public string waveBase, timeBase;

    public void Activate()
    {
        waveText.text = waveBase + GameManager.Instance.currentWave;

        timeText.text = timeBase + GameManager.Instance.ReadTimer();
    }
}
