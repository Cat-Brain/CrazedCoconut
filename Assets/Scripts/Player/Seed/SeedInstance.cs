using UnityEngine;
using System;

[Serializable]
public class SeedInstance
{
    public Seed seed;
    public DeckPile pile;

    public SeedInstance(Seed seed, DeckPile pile = DeckPile.UNSET)
    {
        this.seed = seed;
        this.pile = pile;
    }

    public GameObject Spawn(PlayerPlant player)
    {
        return seed.Spawn(player);
    }

    public static bool IsNull(SeedInstance instance)
    {
        return instance == null || instance.seed == null || instance.pile == DeckPile.UNSET;
    }
}