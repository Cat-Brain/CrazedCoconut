using UnityEngine;

public class PlayerManager : Entity
{
    public Seed[] seedTypes;

    private static PlayerManager instance;
    public static PlayerManager Instance
    { get {
        if (instance == null)
            instance = FindAnyObjectByType<PlayerManager>();
        return instance;
    } }

    public PlayerMove move;
    public PlayerPlant plant;

    public bool active = true;

    public Seed GetRandomSeed()
    {
        return seedTypes[Random.Range(0, seedTypes.Length)];
    }
}
