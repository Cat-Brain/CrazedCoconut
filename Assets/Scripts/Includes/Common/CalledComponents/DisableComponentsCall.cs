using System.Collections.Generic;
using UnityEngine;

public class DisableComponentsCall : MonoBehaviour
{
    public List<Behaviour> toDisable;

    public void Activate()
    {
        foreach (Behaviour component in toDisable)
            component.enabled = false;
    }
}
