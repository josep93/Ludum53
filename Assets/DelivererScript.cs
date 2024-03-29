using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public bool musicFlag = false;

    [SerializeField] State state;
    [SerializeField] Image powerBar;

    [Header("Conexion")]
    [SerializeField] GameObject realPackage;
    [SerializeField] GameObject fakePackage;
    [SerializeField] GameObject cinemachine;
    [SerializeField] GameObject debris;

    [Tooltip("Fuerza de lanzamiento del paquete")]
    [SerializeField] private float force;
    [Tooltip("�ngulo de lanzamiento del paquete")]
    [SerializeField] private float angle;
    [Tooltip("Velocidad Angular del paquete")]
    [SerializeField] private float rotation;
    [SerializeField] private GameObject obstacleController;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        punchingSound = GetComponent<PunchingSoundScript>();
        sprite = GetComponent<SpriteRenderer>();
        state = State.Standby;
        MusicScript.current.SelectTrack(3, true);
        pauseStatus = PauseStatus.Running;
        EventsScript.current.pauseAction += OnPause;
    }

    private void OnDestroy()
    {
        EventsScript.current.pauseAction -= OnPause;
    }
    public void StateUpdate(State state)
    {
        if (state == State.Standby)
        {
            MusicScript.current.SelectTrack(3, true);
            return;
        }
        if (this.state == state) return;
        frameTime = 0;
        this.state = state;
        if (state == State.Prepared)
        {
            if (!musicFlag)
            {
                MusicScript.current.StopMusic();
            }
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
            if (!musicFlag)
            {
                MusicScript.current.SelectTrack(1, false);
            }
            spriteState = 0;
            punchingSound.FinalPunch();
            punchingSound.FinalShout();
            sprite.sprite = deliveringSprites[0];
            PackageScript.current.DeliveryMovement(0);
            CameraPositionScript.current.DramaticPose();
            Invoke(nameof(StartThrowingMusic), 0.5f);
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
        punchingSound.BreakMug();
        yield return new WaitForSeconds(0.2f);
        PackageScript.current.StateChange(PackageScript.State.Ready);
        powerBar.enabled = true;
    }

    private void StartThrowingMusic()
    {
        MusicScript.current.SelectTrack(2, true);
    }

    private void ThrowRealPackage()
    {

        // Activamos el Cinemachine
        cinemachine.SetActive(true);

        // Ativamos el paquete real y desactivamos el falso
        realPackage.SetActive(true);
        fakePackage.SetActive(false);
        if(SceneManager.GetActiveScene().buildIndex == 4)
        MusicScript.current.SelectTrack(4, true);
        else
        MusicScript.current.SelectTrack(2, true);

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            obstacleController.SetActive(true);
            obstacleController.GetComponent<ObstacleControllerScript>().StartObstacle();
        }

        BuildingGeneratorScript.instance.StartGenerate();
        if (BackgroundColorCamara.instance != null)
            BackgroundColorCamara.instance.ChangeColorCamera();

        // Lanzamos el paquete (float force, float angle)
        realPackage.GetComponent<PackageDeliveryScript>().ThrowPackage(force, angle, rotation);
        if (debris == null) return;
        debris.SetActive(true);
    }

}
