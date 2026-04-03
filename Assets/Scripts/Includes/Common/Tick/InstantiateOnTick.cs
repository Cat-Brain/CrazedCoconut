using UnityEngine;

public class InstantiateOnTick : TickComponent
{
    public GameObject toSpawn;

    public bool parentToSelf;
    public bool overrideX, overrideY, overrideZ;
    public float forcedX, forcedY, forcedZ;


    public override void OnTick()
    {
        Instantiate(toSpawn, new Vector3(
            overrideX ? forcedX : transform.position.x,
            overrideY ? forcedY : transform.position.y,
            overrideZ ? forcedZ : transform.position.z)
            , transform.rotation, parentToSelf ? transform : null);
    }
}
