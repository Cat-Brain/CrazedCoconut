using UnityEngine;

public class Pomegranate : MonoBehaviour
{
    public float explosionRadius, explosionForce;
    public float minTimeBeforeDetonate;

    public LayerMask explosionMask;

    void Update()
    {
        minTimeBeforeDetonate -= Time.deltaTime;
    }

    void OnCollisionStay(Collision collision)
    {
        if (minTimeBeforeDetonate <= 0)
            Explode();
    }

    public void Explode()
    {
        if (!enabled)
            return;
        enabled = false;

        GameManager.Instance.SpawnExplosion(transform.position, explosionRadius * 2);
        Collider[] overlapped = Physics.OverlapSphere(transform.position, explosionRadius, explosionMask);
        foreach (Collider col in overlapped)
            if (col.attachedRigidbody != null && transform.position != col.transform.position)
                col.attachedRigidbody.AddForce((col.transform.position - transform.position)
                    .normalized * explosionForce, ForceMode.Impulse);

        Destroy(gameObject);
    }
}
