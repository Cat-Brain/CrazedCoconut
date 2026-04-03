using UnityEngine;
using UnityEngine.InputSystem;
using ClownLib;

public class PlayerMove : MonoBehaviour
{
    public PlayerManager manager;

    public Transform relativeBody;

    public Rigidbody rb;
    public InputActionReference moveXZAction, moveYAction;

    public float accel, speed;

    public enum MoveState
    {
        ACTIVE, STATIONARY, IDLE
    }
    public MoveState moveState = MoveState.ACTIVE;

    void FixedUpdate()
    {
        if (!manager.active || moveState == MoveState.IDLE)
            return;

        (Vector3 rightDir, Vector3 upDir, Vector3 forwardDir, Vector3 localVel) =
            RollUtils.FindLocals(relativeBody.right, rb.angularVelocity);

        Vector3 input = moveXZAction.action.ReadValue<Vector2>().XZ_Y(moveYAction.action.ReadValue<float>());

        if (moveState == MoveState.STATIONARY || input.ZX().sqrMagnitude < 0.01f)
            localVel = localVel.XZ().TrySub2(Time.deltaTime * accel).XZ_Y(localVel.y);
        else
            localVel = localVel.XZ().TryAdd2(Time.deltaTime * accel * input.ZX(), speed).XZ_Y(localVel.y);

        if (moveState == MoveState.STATIONARY || Mathf.Abs(input.y) < 0.1f)
            localVel.y = localVel.y.TrySub(Time.deltaTime * accel);
        else
            localVel.y = localVel.y.TryAdd(Time.deltaTime * accel * input.y, speed);
        

        rb.angularVelocity = rightDir * localVel.x + upDir * localVel.y + forwardDir * localVel.z;
    }

    private void OnDrawGizmos()
    {
        (Vector3 rightDir, Vector3 upDir, Vector3 forwardDir, Vector3 _) =
            RollUtils.FindLocals(relativeBody.right, rb.angularVelocity);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + rightDir);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + upDir);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + forwardDir);
    }
}
