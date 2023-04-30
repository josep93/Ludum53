using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelivererScript : MonoBehaviour
{
    public enum State : byte
    {
        Standby,
        Prepared,
        Delivering,
        Delivered
    }

    enum PauseStatus : byte
    {
        Running,
        Paused
    }

    public static DelivererScript current;

    [SerializeField]Sprite[] deliveringSprites,preparedSprites, standbySprites;
    SpriteRenderer sprite;
    PauseStatus pauseStatus;

    PunchingSoundScript punchingSound;

    int spriteState = 0;
    float frameTime = 0;

    [SerializeField]State state;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        punchingSound = GetComponent<PunchingSoundScript>();
        sprite = GetComponent<SpriteRenderer>();
        state = State.Standby;
        pauseStatus = PauseStatus.Running;
        EventsScript.current.pauseAction += OnPause;
    }

    public void StateUpdate(State state)
    {
        if (state == State.Standby) return;
        if (this.state == state) return;
        frameTime = 0;
        this.state = state;
        if (state == State.Prepared)
        {
            spriteState = 0;
            return;
        }
        if (state == State.Delivered)
        {
            spriteState = 0;
            punchingSound.FinalPunch();
            sprite.sprite = deliveringSprites[0];
            PackageScript.current.DeliveryMovement(0);
            CameraPositionScript.current.DramaticPose();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseStatus == PauseStatus.Paused) return;
        switch (state)
        {
            case State.Prepared:
                frameTime += Time.deltaTime;
                if (frameTime >= 0.5)
                {
                    spriteState = spriteState==1?0:1;
                    frameTime = 0;
                }
                ChangeSprite();
                return;
            case State.Delivering:
                frameTime += Time.deltaTime;
                if (frameTime >= 0.1)
                {
                    var random = Random.Range(0,5);
                    spriteState = random >= spriteState ? random + 1 : random;
                    frameTime = 0;
                    ChangeSprite();
                    punchingSound.CallPunch();
                }
                return;
            case State.Delivered:
                return;
        }
    }

    private void OnPause(bool pause)
    {
        pauseStatus = pause ? PauseStatus.Paused : PauseStatus.Running;
    }

    private void ChangeSprite()
    {
        switch (state)
        {
            case State.Standby:
                sprite.sprite = standbySprites[spriteState];
                return;
            case State.Prepared:
                sprite.sprite = preparedSprites[spriteState];
                PackageScript.current.DeliveryMovement(-1);
                return;
            case State.Delivering:
                sprite.sprite = deliveringSprites[spriteState];
                PackageScript.current.DeliveryMovement(spriteState);
                return;
        }
    }

    public void StartDelivery()
    {
        StartCoroutine("StartDeliveryIenumerator");
    }

    IEnumerator StartDeliveryIenumerator()
    {
        PackageScript.current.DeliveryMovement(-2);
        sprite.sprite = standbySprites[1];
        yield return new WaitForSeconds(0.2f);
        PackageScript.current.StateChange(PackageScript.State.Ready);
    }

}
