using ClownLib;
using UnityEngine;
using static PlayerMove;

public class PlayerGrow : PlayerAbility
{
    public Rigidbody rb;

    public float cooldown;
    private float remainingCooldown = 0;

    public float accel, speed;

    public float baseSize, growSize;
    public float baseMass, growMass;

    public float growFrequency, growDamping;
    private SpringUtils.tDampedSpringMotionParams growSpring = new();

    private float springPosition = 0;
    private float springVelocity = 0;

    private Vector2 direction = Vector2.zero;

    public bool isActive = false;

    public new void Update()
    {
        base.Update();

        remainingCooldown = Mathf.Max(0, remainingCooldown - Time.deltaTime);

        if (held && !isActive && remainingCooldown <= 0)
            Grow();
        else if (!held && isActive)
            Shrink();
            
    }

    void FixedUpdate()
    {
        SpringUtils.CalcDampedSpringMotionParams(ref growSpring, Time.deltaTime, growFrequency, growDamping);
        SpringUtils.UpdateDampedSpringMotion(
            ref springPosition, ref springVelocity, isActive ? 1 : 0, growSpring);

        transform.localScale = Vector3.one * baseSize.Lerp(growSize, springPosition);
        rb.mass = baseMass.Lerp(growMass, springPosition);

        if (!isActive)
            return;

        (Vector3 rightDir, Vector3 upDir, Vector3 forwardDir, Vector3 localVel) =
            RollUtils.FindLocals(manager.move.relativeBody.right, rb.angularVelocity);


        if (direction == Vector2.zero)
        {
            Vector2 input = manager.move.moveXZAction.action.ReadValue<Vector2>();
            if (input.sqrMagnitude >= 0.01f)
                direction = input.normalized;
        }

        if (direction == Vector2.zero)
            localVel = localVel.XZ().TrySub2(Time.deltaTime * accel).XZ_Y(localVel.y);
        else
            localVel = localVel.XZ().TryAdd2(Time.deltaTime * accel * direction.YX(), speed).XZ_Y(localVel.y);

        localVel.y = localVel.y.TrySub(Time.deltaTime * accel);

        rb.angularVelocity = rightDir * localVel.x + upDir * localVel.y + forwardDir * localVel.z;
    }

    public void Grow()
    {
        if (manager.move.moveState == PlayerMove.MoveState.ACTIVE)
            manager.move.moveState = PlayerMove.MoveState.IDLE;

        isActive = true;
    }

    public void Shrink()
    {
        if (manager.move.moveState == PlayerMove.MoveState.IDLE)
            manager.move.moveState = PlayerMove.MoveState.ACTIVE;

        direction = Vector2.zero;
        isActive = false;
        remainingCooldown = cooldown;
    }
}
