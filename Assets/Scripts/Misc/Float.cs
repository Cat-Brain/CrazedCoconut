using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Float : MonoBehaviour
{
    public float buoyancy, damping;

    private Rigidbody rb;

    void FixedUpdate()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (transform.position.y >= GameManager.Instance.waterLevel)
            return;

        rb.AddForce(Vector3.up * buoyancy);
        float dampFactor = GetDampFactor();
        rb.linearVelocity *= dampFactor;
        rb.angularVelocity *= dampFactor;
    }

    public float GetDampFactor()
    {
        return Mathf.Pow(1 - damping, Time.deltaTime);
    }
}
