using ClownLib;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Seed/Seed")]
public class Seed : ScriptableObject
{
    public GameObject prefab;

    public float hVelMul, vVel;
    public bool rotate3D;
    public float maxRotVel;

    public GameObject Spawn(PlayerPlant player)
    {
        GameObject result = Instantiate(prefab, player.transform.position, RandomRotation());

        Rigidbody rb = result.GetComponent<Rigidbody>();
        rb.linearVelocity = (player.rb.linearVelocity.XZ() * hVelMul)
            .XZ_Y(player.rb.linearVelocity.y + vVel);
        rb.angularVelocity = Mathf.Deg2Rad * maxRotVel * RandomRotation().eulerAngles;
        if (Random.Range(0, 2) == 0)
            rb.angularVelocity = rb.angularVelocity.SetX(-rb.angularVelocity.x);
        if (Random.Range(0, 2) == 0)
            rb.angularVelocity = rb.angularVelocity.SetY(-rb.angularVelocity.y);
        if (Random.Range(0, 2) == 0)
            rb.angularVelocity = rb.angularVelocity.SetZ(-rb.angularVelocity.z);

        return result;
    }

    public Quaternion RandomRotation()
    {
        return rotate3D ? Random.rotationUniform
            : Quaternion.Euler(0, Random.Range(-180f, 180f), 0);

    }
}
