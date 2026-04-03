using UnityEngine;

[ExecuteInEditMode]
public class FollowLU : MonoBehaviour
{
    public Transform toFollow;

    private void LateUpdate()
    {
        transform.position = toFollow.position;
    }
}
