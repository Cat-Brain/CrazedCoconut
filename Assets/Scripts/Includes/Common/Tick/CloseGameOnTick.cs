using UnityEngine;

public class CloseGameOnTick : TickComponent
{
    public override void OnTick()
    {
        Application.Quit();
    }
}
