using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatCall : MonoBehaviour
{
    public float buoyancy, damping;

    private Rigidbody rb;

    public void Activate()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

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
