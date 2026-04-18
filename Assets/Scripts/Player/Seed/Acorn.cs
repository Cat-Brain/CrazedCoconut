using ClownLib;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    public Rigidbody rb;

    public float accel, speed;

    private Vector2 dir = Vector2.zero;

    void FixedUpdate()
    {
        if (dir == Vector2.zero)
        {
            if (rb.linearVelocity.magnitude > 0.1f)
                dir = rb.linearVelocity.XZ().normalized;
            else
                dir = Random.onUnitCircle.normalized;
        }

        (Vector3 rightDir, Vector3 upDir, Vector3 forwardDir, Vector3 localVel) =
            RollUtils.FindLocals(dir.Rotate90Clock().XZ_Y(0), rb.angularVelocity);

        localVel = localVel.TryAdd(Time.deltaTime * accel * Vector3.right, speed);
        rb.angularVelocity = rightDir * localVel.x + upDir * localVel.y + forwardDir * localVel.z;
    }
}
