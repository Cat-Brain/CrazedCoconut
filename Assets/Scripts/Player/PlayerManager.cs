using UnityEngine;

public class PlayerManager : Entity
{
    private static PlayerManager instance;
    public static PlayerManager Instance { get
        {
            if (instance == null)
                instance = FindAnyObjectByType<PlayerManager>();
            return instance;
        } }

    public PlayerMove move;
    public PlayerPlant plant;

    public bool active = true;
}
