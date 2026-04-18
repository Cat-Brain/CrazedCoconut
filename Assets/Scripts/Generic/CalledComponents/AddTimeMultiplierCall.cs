using UnityEngine;

public class AddTimeMultiplierCall : MonoBehaviour
{
    public float multiplier;

    public void Activate()
    {
        GameManager.Instance.timeMultipliers.Add(multiplier);
    }
}
