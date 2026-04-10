using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlant : MonoBehaviour
{
    public PlayerManager manager;

    public InputActionReference playAction, discardAction;

    public void Start()
    {
        playAction.action.performed += PlayPressed;
        discardAction.action.performed += DiscardPressed;
    }

    public void OnDestroy()
    {
        playAction.action.performed -= PlayPressed;
        discardAction.action.performed -= DiscardPressed;
    }

    private void PlayPressed(InputAction.CallbackContext context)
    {
        if (!enabled || !manager.active || !context.performed)
            return;

        deck.Play();
    }

    private void DiscardPressed(InputAction.CallbackContext context)
    {
        if (!enabled || !manager.active || !context.performed)
            return;

        deck.Discard();
    }

    public Rigidbody rb;

    public Deck deck;

    public void Activate()
    {
        deck.Play();
    }
}
