using ClownLib;
using UnityEngine;

public class ChargerRollerEnemy : RollerEnemy
{
    public enum ChargeState
    {
        NORMAL, DECEL, JUMP, CHARGE, EXHAUSTED
    }
    public ChargeState chargeState;

    public float chargeDecelTime, chargeJumpTime, chargeDuration, exhaustDuration, chargeCooldown;
    public float chargeRadius, hitForce;

    private float remainingCooldown;

    public string playerTag;

    public float chargeAccel, chargeSpeed;

    private Vector2 chargeDir = Vector2.zero;

    public void Update()
    {
        remainingCooldown = Mathf.Max(0, remainingCooldown - Time.deltaTime);

        switch (chargeState)
        {
            case ChargeState.NORMAL:
                if (remainingCooldown > 0 || Vector3.Distance(player.position, transform.position) > chargeRadius)
                    break;

                remainingCooldown = chargeDecelTime;
                chargeState = ChargeState.DECEL;
                break;
            case ChargeState.DECEL:
                if (remainingCooldown > 0)
                    break;

                remainingCooldown = chargeJumpTime;
                chargeState = ChargeState.JUMP;
                chargeDir = (player.position - transform.position).XZ().normalized;
                rb.linearVelocity = rb.linearVelocity.SetY(-chargeJumpTime * Physics.gravity.y * 0.5f);
                break;
            case ChargeState.JUMP:
                if (remainingCooldown > 0)
                    break;

                remainingCooldown = chargeDuration;
                chargeState = ChargeState.CHARGE;
                break;
            case ChargeState.CHARGE:
                if (remainingCooldown > 0)
                    break;

                remainingCooldown = exhaustDuration;
                chargeState = ChargeState.EXHAUSTED;
                break;
            case ChargeState.EXHAUSTED:
                if (remainingCooldown > 0)
                    break;

                remainingCooldown = chargeCooldown;
                chargeState = ChargeState.NORMAL;
                break;
        }
    }

    public new void FixedUpdate()
    {
        if (chargeState != ChargeState.CHARGE)
        {
            active = chargeState == ChargeState.NORMAL;
            base.FixedUpdate();
            return;
        }

        (Vector3 rightDir, Vector3 upDir, Vector3 forwardDir, Vector3 localVel) =
            RollUtils.FindLocals(chargeDir.Rotate90Clock().XZ_Y(0), rb.angularVelocity);

        localVel = localVel.TryAdd(Time.deltaTime * chargeAccel * Vector3.right, chargeSpeed);

        rb.angularVelocity = rightDir * localVel.x + upDir * localVel.y + forwardDir * localVel.z;
    }

    public void OnCollision(Collision collision)
    {
        if (chargeState != ChargeState.CHARGE || !collision.rigidbody || !collision.rigidbody.CompareTag(playerTag))
            return;

        foreach (ContactPoint contact in collision.contacts)
            collision.rigidbody.AddForceAtPosition(
                -hitForce / collision.contactCount * contact.normal, contact.point, ForceMode.Impulse);

        Debug.Log(collision.contactCount);

        remainingCooldown = exhaustDuration;
        chargeState = ChargeState.EXHAUSTED;
    }

    public void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision);
    }

    public void OnCollisionStay(Collision collision)
    {
        OnCollision(collision);
    }

    public new void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chargeRadius);
    }
}
