using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlant : MonoBehaviour
{
    public PlayerManager manager;

    public string plantTag;

    public InputActionReference playAction, discardAction;

    public void Start()
    {
        playAction.action.performed += PlayPressed;
        discardAction.action.performed += DiscardPressed;

        GameManager.Instance.combatExit.AddListener(CleanPlants);
    }

    public void OnDestroy()
    {
        playAction.action.performed -= PlayPressed;
        discardAction.action.performed -= DiscardPressed;

        GameManager.Instance?.combatExit?.RemoveListener(CleanPlants);
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

    public void CleanPlants()
    {
        foreach (GameObject plant in GameObject.FindGameObjectsWithTag(plantTag))
        {
            GameManager.SpawnSmoke(plant.transform.position, plant.transform.localScale.x);
            plant.SetActive(false);
            Destroy(plant);
        }
    }
}
