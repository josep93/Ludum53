using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PackageScript : MonoBehaviour
{
    enum State : byte
    {
        Ready,
        Delivered,
        Received
    }

    enum PauseStatus : byte
    {
        Playing,
        Paused
    }

    int power = 0;
    [SerializeField] TextMeshProUGUI powerText;

    InputSystem inputActions;

    State state;
    PauseStatus pauseStatus;
    // Start is called before the first frame update
    void Start()
    {
        inputActions = new InputSystem();
        StateChange(State.Ready);
        pauseStatus = PauseStatus.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseStatus == PauseStatus.Paused)
        {
            return;
        }
        switch (state)
        {
            case State.Ready:
                ReadyUpdate();
                return;
        }
    }

    private void StateChange(State state)
    {
        this.state = state;
        switch (state)
        {
            case State.Ready:
                inputActions.Disable();
                inputActions.Ready.Enable();
                inputActions.Ready.Throw.performed += Throw;
                return;
        }
    }

    private void ReadyUpdate()
    {
        if (Time.frameCount % 30 == 0)
        {
            power -= power / 40 + 1;
            power = power > 0 ? power : 0;
        }
        if (power >= 100)
        {
            power = 100;
            StateChange(State.Delivered);
        }
        powerText.text = "Power: " + power + "%";
    }

    private void Throw(InputAction.CallbackContext ctx)
    {
        if (pauseStatus == PauseStatus.Playing)
        {
            power += 7;
        }
    }
}