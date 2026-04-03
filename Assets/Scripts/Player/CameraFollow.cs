using ClownLib;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector2 offset;
    public float frequency, damping;
    public SpringUtils.tDampedSpringMotionParams spring = new();


    public bool following;
    public Vector2 desiredPos;

    private Vector2 velocity = Vector2.zero;

    void LateUpdate()
    {
        SpringUtils.CalcDampedSpringMotionParams(ref spring, Time.deltaTime, frequency, damping);

        if (following)
            desiredPos = player.position.XZ();

        Vector2 currentPos = transform.position.XZ();

        SpringUtils.UpdateDampedSpringMotion(
            ref currentPos.x, ref velocity.x, desiredPos.x + offset.x, spring);
        SpringUtils.UpdateDampedSpringMotion(
            ref currentPos.y, ref velocity.y, desiredPos.y + offset.y, spring);

        transform.position = currentPos.XZ_Y(transform.position.y);
    }
}
