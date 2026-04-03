using UnityEngine;

public class PlayerManager : Entity
{
    public PlayerMove move;
    public PlayerAbility ability;

    public bool active = true;

    void Awake()
    {
        GameManager.SetPlayerManager(this);
    }
}
