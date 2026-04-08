using System.Collections.Generic;
using UnityEngine;

public enum DeckPile
{
    UNSET, DECK, DRAW, DISCARD, EXHAUST, HAND
}

public class Deck : MonoBehaviour
{
    public PlayerPlant player;

    public List<Seed> startingDeck;
    public List<SeedInstance> deck = new();
    public List<SeedInstance> draw = new(), discard = new(), exhaust = new();
    public SeedInstance hand = null;

    public float timeTillDraw = 0;

    public void AddSeed(Seed seed)
    {
        deck.Add(new SeedInstance(seed, DeckPile.DECK));
    }

    public ref List<SeedInstance> GetPile(DeckPile pile)
    {
        switch (pile)
        {
            case DeckPile.DECK:
                return ref deck;
            case DeckPile.DRAW:
                return ref draw;
            case DeckPile.DISCARD:
                return ref discard;
            case DeckPile.EXHAUST:
                return ref exhaust;
            default:
                Debug.LogError("Invalid Deck Pile!");
                return ref deck;
        }
    }

    public void MoveSeed(int index, DeckPile from, DeckPile to)
    {
        List<SeedInstance> fromPile = GetPile(from),
            toPile = GetPile(to);

        fromPile[index].pile = to;
        toPile.Add(fromPile[index]);
        fromPile.RemoveAt(index);
    }

    public void DrawSeed(int index, DeckPile from, DeckPile overflow)
    {
        if (hand != null)
            DiscardHand(overflow);

        List<SeedInstance> fromPile = GetPile(from);

        if (fromPile.Count <= 0)
            TransferCards(overflow, from);

        if (index < 0 || index >= fromPile.Count)
        {
            Debug.LogError("Invalid Draw Seed Index!");
            return;
        }

        fromPile[index].pile = DeckPile.HAND;
        hand = fromPile[index];
        fromPile.RemoveAt(index);
    }

    public void TransferCards(DeckPile from, DeckPile to)
    {
        List<SeedInstance> fromPile = GetPile(from),
            toPile = GetPile(to);

        for (int i = 0; i < fromPile.Count; i++)
        {
            fromPile[i].pile = to;
            toPile.Add(fromPile[i]);
        }
        fromPile.Clear();
    }

    public void DiscardHand(DeckPile to)
    {
        List<SeedInstance> toPile = GetPile(to);

        hand.pile = to;
        toPile.Add(hand);
        hand = null;
    }

    public void ResetPiles()
    {
        draw.Clear();
        discard.Clear();
        exhaust.Clear();
        hand = null;
    }

    public void EnterCombat()
    {
        ResetPiles();

        foreach (SeedInstance seed in deck)
            draw.Add(new SeedInstance(seed.seed, DeckPile.DRAW));

        timeTillDraw = 0;
    }

    public void ExitCombat()
    {
        ResetPiles();
    }

    public GameObject PlayCard()
    {
        if (hand == null)
            return null;

        SeedInstance oldHand = hand;
        DiscardHand(DeckPile.DISCARD);
        return oldHand.Spawn(player);
    }

    void Awake()
    {
        GameManager.GetInstance().combatEnter.AddListener(EnterCombat);

        foreach (Seed seed in startingDeck)
            deck.Add(new SeedInstance(seed, DeckPile.DECK));
    }

    void Update()
    {
        timeTillDraw = Mathf.Max(0, timeTillDraw - Time.deltaTime);
        if (hand == null && timeTillDraw <= 0)
            DrawSeed(Random.Range(0, draw.Count), DeckPile.DRAW, DeckPile.DISCARD);
    }
}
