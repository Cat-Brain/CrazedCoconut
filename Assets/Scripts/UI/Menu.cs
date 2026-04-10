using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public string internalName;

    public UnityEvent activate, deactivate;

    public bool active;

    public void SetActivate(bool active)
    {
        if (active == this.active)
            return;

        this.active = active;
        (active ? activate : deactivate)?.Invoke();
    }
}
