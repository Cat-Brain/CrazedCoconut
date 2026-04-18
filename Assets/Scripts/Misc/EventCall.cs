using UnityEngine;
using UnityEngine.Events;

public class EventCall : MonoBehaviour
{
    public UnityEvent onActivate;

    public void Activate()
    {
        onActivate?.Invoke();
    }
}
