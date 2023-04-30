using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] Sprite[] deliveringSprites, preparedSprites, standbySprites;
    SpriteRenderer sprite;
    PauseStatus pauseStatus;

    PunchingSoundScript punchingSound;

    int spriteState = 0;
    float frameTime = 0;

    [SerializeField] State state;
    [SerializeField] Image powerBar;

    [Header("Conexion")]
    [SerializeField] GameObject realPackage;
    [SerializeField] GameObject fakePackage;
    [SerializeField] GameObject cinemachine;
    [SerializeField] GameObject debris;

    [Tooltip("Fuerza de lanzamiento del paquete")]
    [SerializeField] private float force;
    [Tooltip("Ángulo de lanzamiento del paquete")]
    [SerializeField] private float angle;

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
            MusicScript.current.StopMusic();
            spriteState = 0;
            return;
        }
        if (state == State.Delivering)
        {
            MusicScript.current.SelectTrack(0, true);
        }
        if (state == State.Delivered)
        {
            SpaceBarPromptScript.current.TurnOffPrompt();
            MusicScript.current.SelectTrack(1, false);
            spriteState = 0;
            punchingSound.FinalPunch();
            punchingSound.FinalShout();
            sprite.sprite = deliveringSprites[0];
            PackageScript.current.DeliveryMovement(0);
            CameraPositionScript.current.DramaticPose();
            Invoke(nameof(ThrowRealPackage), 1.0f);
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
                    spriteState = spriteState == 1 ? 0 : 1;
                    frameTime = 0;
                }
                ChangeSprite();
                return;
            case State.Delivering:
                frameTime += Time.deltaTime;
                if (frameTime >= 0.1)
                {
                    var random = Random.Range(0, 5);
                    spriteState = random >= spriteState ? random + 1 : random;
                    frameTime = 0;
                    ChangeSprite();
                    punchingSound.CallPunch();
                    if (Random.Range(0, 4) == 3)
                    {
                        punchingSound.CallShout();
                    }
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
                spriteState = spriteState > preparedSprites.Length - 1 ? 0 : spriteState;
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
        punchingSound.Shout();
        yield return new WaitForSeconds(0.2f);
        PackageScript.current.StateChange(PackageScript.State.Ready);
        powerBar.enabled = true;
    }


    private void ThrowRealPackage()
    {

        // Activamos el Cinemachine
        cinemachine.SetActive(true);

        // Ativamos el paquete real y desactivamos el falso
        realPackage.SetActive(true);
        fakePackage.SetActive(false);

        ObstacleControllerScript.instance.StartObstacle();

        //realPackage.transform.SetPositionAndRotation(fakePackage.transform.position, fakePackage.transform.rotation);
        // Lanzamos el paquete (float force, float angle)
        realPackage.GetComponent<PackageDeliveryScript>().ThrowPackage(force, angle);
        if (debris == null) return;
        debris.SetActive(true);
    }

}
