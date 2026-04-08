using System.Collections.Generic;
using UnityEngine;

public class PlayerPlant : PlayerAbility
{
    public Rigidbody rb;

    public Deck deck;

    public override void Activate()
    {
        deck.PlayCard();
    }
}
