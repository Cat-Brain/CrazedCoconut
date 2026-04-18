using UnityEngine;
using UnityEngine.Events;

public class EventOnStart : MonoBehaviour
{
    public UnityEvent onStart;

    void Start()
    {
        onStart?.Invoke();
    }
}
