using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbility : MonoBehaviour
{
    public PlayerManager manager;

    public InputActionReference action;

    public bool held;

    public void Start()
    {
        action.action.performed += OnPress;
        action.action.canceled += OnRelease;
    }

    public void OnDestroy()
    {
        action.action.performed -= OnPress;
        action.action.canceled -= OnRelease;
    }

    public void Update()
    {
        held = action.action.IsPressed() && manager.active;
    }

    private void OnPress(InputAction.CallbackContext context)
    {
        if (enabled && manager.active && context.performed)
            Activate();
    }

    private void OnRelease(InputAction.CallbackContext context)
    {
        if (enabled && manager.active && context.performed)
            Deactivate();
    }

    public virtual void Activate() { }
    public virtual void Deactivate() { }
}
