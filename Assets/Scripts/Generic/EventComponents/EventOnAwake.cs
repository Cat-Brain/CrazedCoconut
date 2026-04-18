using UnityEngine;
using UnityEngine.Events;

public class EventOnAwake: MonoBehaviour
{
    public UnityEvent onAwake;

    void Awake()
    {
        onAwake?.Invoke();
    }
}
