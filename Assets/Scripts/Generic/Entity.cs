using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public UnityEvent onDeath;

    public bool alive = true;

    public void Die()
    {
        if (!alive)
            return;

        alive = false;
        OnDeath();
    }

    public virtual void OnDeath()
    {
        onDeath?.Invoke();
    }
}
