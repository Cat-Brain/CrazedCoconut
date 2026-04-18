using UnityEngine;

public class TurnToPlayer : MonoBehaviour
{
    public float slerpSpeed;
    // REPLACE ALL OF THIS LATER!!!

    void Update()
    {
        Vector3 direction = (PlayerManager.Instance.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
            slerpSpeed * Time.deltaTime);
    }
}
