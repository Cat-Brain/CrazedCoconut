using UnityEngine;

public abstract class TickComponent : MonoBehaviour
{
    public int tickIndex;

    public void Tick(int tickIndex)
    {
        if (this.tickIndex == tickIndex)
            OnTick();
    }

    public abstract void OnTick();
}
