using System.Collections.Generic;
using UnityEngine;

public class CocoBoomer : RollerEnemy
{
    public float timePerExplosion;
    public float explosionForce, detectionRadius, explosionRadius;
    public LayerMask explosionMask;

    private float timeTillNextExplosion = 0;

    void Update()
    {
        if ((timeTillNextExplosion -= Time.deltaTime) <= 0 &&
            Physics.CheckSphere(transform.position, detectionRadius, explosionMask))
        {
            timeTillNextExplosion = timePerExplosion;
            GameManager.Instance.SpawnExplosion(transform.position, explosionRadius * 2);
            Collider[] overlapped = Physics.OverlapSphere(transform.position, explosionRadius, explosionMask);
                foreach (Collider col in overlapped)
                    if (col.attachedRigidbody != null && transform.position != col.transform.position)
                        col.attachedRigidbody.AddForce((col.transform.position - transform.position)
                            .normalized * explosionForce, ForceMode.Impulse);
        }
    }
    //public float timePerExplosion, explosionDelay;
    //public float explosionForce, explosionRadius;
    //public LayerMask explosionMask;

    //private float timeTillNextExplosion = 0;
    //private List<(Vector3 position, float timeTill)> storedExplosions = new();

    //void Update()
    //{
    //    if ((timeTillNextExplosion -= Time.deltaTime) <= 0)
    //    {
    //        storedExplosions.Add((transform.position, explosionDelay));
    //        timeTillNextExplosion = timePerExplosion;
    //    }
    //    for (int i = 0; i < storedExplosions.Count; i++)
    //        if ((storedExplosions[i] = (storedExplosions[i].position,
    //            storedExplosions[i].timeTill - Time.deltaTime)).timeTill <= 0)
    //        {
    //            Vector3 position = storedExplosions[i].position;
    //            GameManager.Instance.SpawnExplosion(position, explosionRadius * 2);
    //            Collider[] overlapped = Physics.OverlapSphere(position, explosionRadius, explosionMask);
    //            foreach (Collider col in overlapped)
    //                if (col.attachedRigidbody != null && position != col.transform.position)
    //                    col.attachedRigidbody.AddForce((col.transform.position - position)
    //                        .normalized * explosionForce, ForceMode.Impulse);
    //        }
    //    storedExplosions.RemoveAll(((Vector3 position, float timeTill) explosion)
    //        => explosion.timeTill <= 0);
    //}
}
