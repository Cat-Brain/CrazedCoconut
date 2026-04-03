using System.Collections.Generic;
using UnityEngine;

public class TickEntity : MonoBehaviour
{
    [HideInInspector] public TickComponent[] tickComponents;

    public void Awake()
    {
        tickComponents = GetComponents<TickComponent>();
    }

    public virtual void Tick(int tickIndex)
    {
        if (enabled)
            foreach (TickComponent tickComponent in tickComponents)
                tickComponent.Tick(tickIndex);
    }
}
