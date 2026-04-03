using ClownLib;
using UnityEngine;

public class PlayerExplode : PlayerAbility
{
    public float cooldown;
    private float remainingCooldown = 0;

    public float explosionRadius, explosionForce;
    public GameObject explosionPrefab;

    public new void Update()
    {
        base.Update();

        remainingCooldown = Mathf.Max(0, remainingCooldown - Time.deltaTime);
    }

    public override void Activate()
    {
        if (remainingCooldown > 0)
            return;

        remainingCooldown = cooldown;

        foreach (Collider col in Physics.OverlapSphere(transform.position, explosionRadius))
            if (col.gameObject != gameObject && col.attachedRigidbody)
                col.attachedRigidbody.AddForce(
                    (col.transform.position - transform.position).normalized * explosionForce,
                    ForceMode.Impulse);

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
