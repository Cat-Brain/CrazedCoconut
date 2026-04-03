using UnityEngine;

[RequireComponent(typeof(TickEntity))]
public class TickOnTriggerEnter : MonoBehaviour
{
    public int tickIndex;
    public string desiredTag;

    private TickEntity tickEntity;

    private void Awake()
    {
        tickEntity = GetComponent<TickEntity>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(desiredTag))
            tickEntity.Tick(tickIndex);
    }
}
