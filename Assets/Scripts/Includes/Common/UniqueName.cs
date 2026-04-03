using UnityEngine;

public class UniqueName : MonoBehaviour
{
    void Awake()
    {
        foreach (UniqueName other in FindObjectsByType<UniqueName>())
            if (this != other && gameObject.name == other.gameObject.name)
                Destroy(gameObject);
    }
}
