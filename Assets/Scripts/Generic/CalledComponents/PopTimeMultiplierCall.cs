using UnityEngine;

public class PopTimeMultiplierCall : MonoBehaviour
{
    public void Activate()
    {
        GameManager.Instance.timeMultipliers.RemoveAt(GameManager.Instance.timeMultipliers.Count - 1);
    }
}
