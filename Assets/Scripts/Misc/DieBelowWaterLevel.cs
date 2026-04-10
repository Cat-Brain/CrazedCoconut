using UnityEngine;

[RequireComponent(typeof(Entity))]
public class DieBelowWaterLevel : MonoBehaviour
{
    private Entity entity;


    void FixedUpdate()
    {
        if (entity == null)
            entity = GetComponent<Entity>();

        if (transform.position.y < GameManager.Instance.waterLevel)
            entity.Die();
    }
}
