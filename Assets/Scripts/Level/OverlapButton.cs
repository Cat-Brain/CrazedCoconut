using ClownLib;
using UnityEngine;
using UnityEngine.Events;

public class OverlapButton : MonoBehaviour
{
    public UnityEvent onOverlap;

    public LayerMask desiredMask;
    public float overlapTime;
    public float elapsedTime = 0;

    void FixedUpdate()
    {
        elapsedTime = Mathf.Max(0, elapsedTime - Time.deltaTime);

        if (elapsedTime >= overlapTime)
            onOverlap?.Invoke();
    }

    void OnTriggerStay(Collider other)
    {
        if (CMath.LayerOverlapsMask(other.gameObject.layer, desiredMask))
            elapsedTime += Time.deltaTime * 2;
    }
}
