using ClownLib;
using UnityEngine;

public class PlayerDash : PlayerAbility
{
    public Collider col;
    public Rigidbody rb;

    public float cooldown;
    private float remainingCooldown = 0;

    public float dashDuration;
    private float remainingDash = 0;

    public float dashMass;
    private float baseMass;

    public PhysicsMaterial dashMat;
    private PhysicsMaterial baseMat;

    public float dashForce, dashSpeed;

    public bool isActive = false;

    private void Awake()
    {
        baseMass = rb.mass;
        baseMat = col.material;
    }

    public new void Update()
    {
        base.Update();

        remainingCooldown = Mathf.Max(0, remainingCooldown - Time.deltaTime);
        remainingDash = Mathf.Max(0, remainingDash - Time.deltaTime);

        if (isActive && remainingDash <= 0)
            EndDash();
    }

    public override void Activate()
    {
        Vector2 input = manager.move.moveXZAction.action.ReadValue<Vector2>();
        if (isActive || remainingCooldown > 0 || manager.move.moveXZAction.action.ReadValue<Vector2>().sqrMagnitude < 0.01f)
            return;

        StartDash(input.normalized);
    }

    public void StartDash(Vector2 direction)
    {
        if (manager.move.moveState == PlayerMove.MoveState.ACTIVE)
            manager.move.moveState = PlayerMove.MoveState.IDLE;

        rb.mass = dashMass;
        col.material = dashMat;

        rb.linearVelocity = rb.linearVelocity.XZ()
            .TryAdd2(direction * dashForce, dashSpeed).XZ_Y(rb.linearVelocity.y);

        remainingDash = dashDuration;
        isActive = true;
    }

    public void EndDash()
    {
        if (manager.move.moveState == PlayerMove.MoveState.IDLE)
            manager.move.moveState = PlayerMove.MoveState.ACTIVE;

        rb.mass = baseMass;
        col.material = baseMat;

        isActive = false;
        remainingCooldown = cooldown;
    }
}
