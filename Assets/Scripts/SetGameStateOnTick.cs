using UnityEngine;

public class SetGameStateOnTick : TickComponent
{
    public GameState gameState;

    public override void OnTick()
    {
        GameManager.SetGameState(gameState);
    }
}