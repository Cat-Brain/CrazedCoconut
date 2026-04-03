using ClownLib;
using UnityEngine;

public class RollerEnemy : Enemy
{
    public Rigidbody rb;

    public float accel, decel, speed;

    public bool active = true;

    public void FixedUpdate()
    {
        Vector3 relativeRight = Vector3.Cross(Vector3.up, player.position - transform.position).normalized;

        (Vector3 rightDir, Vector3 upDir, Vector3 forwardDir, Vector3 localVel) =
            RollUtils.FindLocals(relativeRight, rb.angularVelocity);

        if (active)
            localVel = localVel.TryAdd(Time.deltaTime * accel * Vector3.right, speed);
        else
            localVel = localVel.TrySub(Time.deltaTime * decel);

        rb.angularVelocity = rightDir * localVel.x + upDir * localVel.y + forwardDir * localVel.z;
    }

    public void OnDrawGizmos()
    {
        Vector3 relativeRight = Vector3.Cross(player.position - transform.position, Vector3.up).normalized;

        (Vector3 rightDir, Vector3 upDir, Vector3 forwardDir, Vector3 _) =
            RollUtils.FindLocals(relativeRight, rb.angularVelocity);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + rightDir);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + upDir);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + forwardDir);
    }
}
