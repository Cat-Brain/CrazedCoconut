using UnityEngine;

public class Entity : MonoBehaviour
{
    public TickEntity tickEntity;

    public int deathTick;

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
        tickEntity.Tick(deathTick);
    }
}
