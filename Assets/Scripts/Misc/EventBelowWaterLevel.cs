using UnityEngine;
using UnityEngine.Events;

public class EventBelowWaterLevel : MonoBehaviour
{
    public UnityEvent onBelow;


    void FixedUpdate()
    {
        if (transform.position.y < GameManager.Instance.waterLevel)
            onBelow?.Invoke();
    }
}
