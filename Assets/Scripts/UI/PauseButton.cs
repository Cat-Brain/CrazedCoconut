using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseButton : MonoBehaviour
{
    public InputActionReference pauseAction;
    public GameObject pauseCanvas;

    public UnityEvent startPause, endPause;

    void Start()
    {
        pauseAction.action.started += OnPausePress;
    }

    public void OnPausePress(InputAction.CallbackContext context)
    {
        (pauseCanvas.activeSelf ? endPause : startPause)?.Invoke();
    }
}
