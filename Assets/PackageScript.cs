using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;

public class PackageScript : MonoBehaviour
{
    public enum State : byte
    {
        StandBy,
        Ready,
        Delivered,
        Received
    }

    enum PauseStatus : byte
    {
        Running,
        Paused
    }

    public static PackageScript current;

    int power = 0;
    int acumulatedLaziness = 0;

    bool awaitingResponse = false;

    [SerializeField] RectTransform powerbar;
    [SerializeField] Image powerBarColor;
    [SerializeField] Color[] colors;
    float powerBarMaxWidth;

    [SerializeField] TextMeshProUGUI powerText;

    InputSystem inputActions;
    public SpriteRenderer sprite;
    
    State state;
    PauseStatus pauseStatus;

    [SerializeField] Vector3[] packagePosition;
    [SerializeField] bool[] up;
    Vector3 basePosition;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        basePosition = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        powerBarMaxWidth = powerbar.sizeDelta.x;
        powerbar.sizeDelta = new Vector2(0, powerbar.sizeDelta.y);
        inputActions = new InputSystem();
        StateChange(State.StandBy);
        pauseStatus = PauseStatus.Running;
        EventsScript.current.pauseAction += OnPause;
    }

    private void OnDestroy()
    {
        try
        {
            EventsScript.current.pauseAction -= OnPause;
            if (this.state == State.StandBy) inputActions.Ready.Throw.performed -= StartDelivery;
            if (this.state == State.Ready) inputActions.Ready.Throw.performed -= Throw;

        }
        catch (Exception e) { }
        
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
            case State.Delivered:
                DeliveredUpdate();
                return;
        }
    }

    public void StateChange(State state)
    {
        if (this.state == state && state != State.StandBy) return;
        if (this.state == State.StandBy) inputActions.Ready.Throw.performed -= StartDelivery;
        if (this.state == State.Ready) inputActions.Ready.Throw.performed -= Throw;
        this.state = state;
        switch (state)
        {
            case State.StandBy:
                inputActions.Disable();
                inputActions.Ready.Enable();
                inputActions.Ready.Throw.performed += StartDelivery;
                return;
            case State.Ready:
                awaitingResponse = false;
                inputActions.Disable();
                inputActions.Ready.Enable();
                inputActions.Ready.Throw.performed += Throw;
                return;
            case State.Delivered:
                inputActions.Disable();
                DelivererScript.current.StateUpdate(DelivererScript.State.Delivered);
                return;
        }
    }

    private void ReadyUpdate()
    {
        if (Time.frameCount % 15 == 0)
        {
            var acumulatedEffect = acumulatedLaziness>2?acumulatedLaziness:0;
            power -= (power / 40 + 1 + acumulatedEffect)/2;
            power = power > 0 ? power : 0;
            acumulatedLaziness++;
        }
        if (power >= 100)
        {
            power = 100;
            StateChange(State.Delivered);
        }
        powerbar.sizeDelta= new Vector2 (power/100f*powerBarMaxWidth,powerbar.sizeDelta.y);
        SelectBarColor();
        powerText.text = "Power: " + power + "%";
        if (power <= 0)
        {
            DelivererScript.current.StateUpdate(DelivererScript.State.Prepared);
        } else if (power > 0 && power < 100)
        {
            DelivererScript.current.StateUpdate(DelivererScript.State.Delivering);
        }
    }

    private void DeliveredUpdate()
    {
        
    }

    private void SelectBarColor()
    {
        powerBarColor.color = Color.Lerp(colors[0], colors[1],power/100f);
        return;
    }

    private void Throw(InputAction.CallbackContext ctx)
    {
        if (pauseStatus == PauseStatus.Running)
        {
            power += 7;
            acumulatedLaziness = 0;
        }
    }

    private void StartDelivery(InputAction.CallbackContext ctx)
    {
        if (awaitingResponse) return;
        DelivererScript.current.StartDelivery();
        awaitingResponse = true;
    }

    private void OnPause(bool pause)
    {
        pauseStatus = pause ? PauseStatus.Paused : PauseStatus.Running;
    }

    public void DeliveryMovement(int position)
    {
        if (position == -1)
        {
            transform.position = basePosition;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            sprite.sortingOrder = 1;
            return;
        }
        if (position == -2)
        {
            transform.position = basePosition;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            sprite.sortingOrder = -1;
            return;
        }
        transform.localPosition = packagePosition[position];
        transform.rotation = Quaternion.Euler(new Vector3(0,0, UnityEngine.Random.Range(-50,50)));
        sprite.sortingOrder = up[position] ? 1 : -1;
    }
}
