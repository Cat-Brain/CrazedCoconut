using UnityEngine;

public class ControlTimer : MonoBehaviour
{
    public bool startTimer;

    public void Activate()
    {
        if (startTimer)
            GameManager.Instance.StartTimer();
        else
            GameManager.Instance.EndTimer();
    }
}
