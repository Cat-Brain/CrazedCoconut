using UnityEngine;
using UnityEngine.Events;

public class EventOnDestroy: MonoBehaviour
{
    public UnityEvent onDestroy;

    public void OnDestroy()
    {
        onDestroy?.Invoke();
        
    }
}
