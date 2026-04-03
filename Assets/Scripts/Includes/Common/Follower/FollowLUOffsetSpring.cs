using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;


[ExecuteInEditMode]
public class FollowLUOffsetSpring : MonoBehaviour
{
    public bool setOffsetOnAwake;
    [Tooltip("If true uses Time.unscaledDeltaTime. If false uses Time.deltaTime")]
    public bool useUnscaled;
    public Vector3 offset;

    public Transform toFollow;

    public float frequency, damping;

    public Vector3 velocity;
    public SpringUtils.tDampedSpringMotionParams spring = new();

    private void Awake()
    {
        if (setOffsetOnAwake)
            UpdateOffset();
    }

    private void LateUpdate()
    {
        if (toFollow != null)
        {
            SpringUtils.CalcDampedSpringMotionParams(ref spring,
                useUnscaled ? Time.unscaledDeltaTime : Time.deltaTime, frequency, damping);

            Vector3 pos = transform.position, desiredPos = toFollow.position + offset;

            SpringUtils.UpdateDampedSpringMotion(ref pos.x, ref velocity.x, desiredPos.x, spring);
            SpringUtils.UpdateDampedSpringMotion(ref pos.y, ref velocity.y, desiredPos.y, spring);
            SpringUtils.UpdateDampedSpringMotion(ref pos.z, ref velocity.z, desiredPos.z, spring);

            transform.position = pos;
        }
    }

    [ProButton]
    public void UpdateOffset()
    {
        if (toFollow != null)
            offset = transform.position - toFollow.position;
    }
}