using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;


[ExecuteInEditMode]
public class FollowLUOffset : MonoBehaviour
{
    public bool setOffsetOnAwake;
    public Vector3 offset;

    public Transform toFollow;

    private void Awake()
    {
        if (setOffsetOnAwake)
            UpdateOffset();
    }

    private void LateUpdate()
    {
        transform.position = toFollow.position + offset;
    }

    [ProButton]
    public void UpdateOffset()
    {
        offset = transform.position - toFollow.position;
    }
}