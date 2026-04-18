using UnityEngine;

public class ApplyMenuPathCall : MonoBehaviour
{
    public string path;

    public void Activate()
    {
        GameManager.Instance.ApplyMenuPath(path);
    }
}
