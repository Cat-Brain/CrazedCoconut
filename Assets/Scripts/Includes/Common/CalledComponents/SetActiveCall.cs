using UnityEngine;

public class SetActiveCall : MonoBehaviour
{
    [Tooltip("If null uses this GameObject")]
    public GameObject toSet;
    public bool active;

    public void Activate()
    {
        (toSet ? toSet : gameObject).SetActive(active);
    }
}
