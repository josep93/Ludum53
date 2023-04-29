using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventsScript : MonoBehaviour
{
    public static EventsScript current;
    public static bool paused;

    [SerializeField] GameObject panel;

    InputSystem inputActions;

    public Action<bool> pauseAction;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        pauseAction += OnPause;
        inputActions = new InputSystem();
        inputActions.Pause.Enable();
        inputActions.Pause.Pause.performed += PausePressed;
    }

    private void OnPause(bool pause)
    {
        paused = pause;
        panel.SetActive(pause);
    }

    private void PausePressed(InputAction.CallbackContext ctx)
    {
        pauseAction(!paused);
    }
}
