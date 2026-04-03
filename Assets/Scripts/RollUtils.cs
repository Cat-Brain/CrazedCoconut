using UnityEngine;

public class RollUtils : MonoBehaviour
{
    public static (Vector3 rightDir, Vector3 upDir, Vector3 forwardDir,
        Vector3 localVel) FindLocals(Vector3 baseRight, Vector3 baseVelocity)
    {
        Vector3 rightDir = baseRight, upDir = Vector3.up, forwardDir =
            Vector3.Cross(Vector3.up, baseRight).normalized;

        return (rightDir, upDir, forwardDir, new(
            Vector3.Dot(rightDir, baseVelocity),
            Vector3.Dot(upDir, baseVelocity),
            Vector3.Dot(forwardDir, baseVelocity)));
    }
}
