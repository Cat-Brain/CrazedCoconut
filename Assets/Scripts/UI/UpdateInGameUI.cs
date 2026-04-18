using TMPro;
using UnityEngine;

public class UpdateInGameUI : MonoBehaviour
{
    public TextMeshProUGUI drawText, discardText, handText, waveText, timerText;
    public string baseDraw, baseDiscard, baseHand, baseWave, baseTimer;

    void LateUpdate()
    {
        Deck deck = PlayerManager.Instance.plant.deck;
        bool handNull = SeedInstance.IsNull(deck.hand);
        if (handNull && deck.draw.Count == 0 && deck.discard.Count == 0)
            drawText.enabled = discardText.enabled = handText.enabled = false;
        else
        {
            drawText.enabled = discardText.enabled = true;
            drawText.text = baseDraw + deck.draw.Count;
            discardText.text = baseDiscard + deck.discard.Count;
            if (handNull)
                handText.enabled = false;
            else
            {
                handText.enabled = true;
                handText.text = baseHand + deck.hand.seed.name;
            }
        }

        waveText.text = baseWave + GameManager.Instance.currentWave;
        timerText.text = baseTimer + GameManager.Instance.ReadTimer();
    }
}
