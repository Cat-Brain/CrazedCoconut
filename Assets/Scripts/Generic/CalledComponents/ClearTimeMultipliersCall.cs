using UnityEngine;

public class ClearTimeMultipliersCall : MonoBehaviour
{
    public void Activate()
    {
        GameManager.Instance.timeMultipliers.Clear();
    }
}
