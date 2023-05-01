using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AirPackageScript : MonoBehaviour
{
    InputSystem inputActions;
    public State state=State.Flying;
    Rigidbody2D rb;
    Color parryColor;
    SpriteRenderer spriteRenderer;
    AudioSource audio;
    private InputSystem input;

    public static bool lost = false; 

    [SerializeField]GameObject losePanel;
    public enum State : byte
    {
        Flying,
        Parrying,
        Recovering,
        Stopping,
        Stopped
    }
    // Start is called before the first frame update
    private void Start()
    {
        lost = false;
        input = new();
        audio = GetComponents<AudioSource>()[1]; 
        spriteRenderer = GetComponent<SpriteRenderer>();
        ColorUtility.TryParseHtmlString("#FFBB84", out parryColor);
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem();
        inputActions.Air.Enable();
        inputActions.Air.Stop.performed += ForceStop;
        inputActions.Air.Parry.performed += Parry;
    }


    private void OnDestroy()
    {
        try
        {
            inputActions.Air.Stop.performed -= ForceStop;
            inputActions.Air.Parry.performed -= Parry;
            inputActions.Air.Disable();
        }
        catch (Exception e)
        {}
        
    }

    void Parry(InputAction.CallbackContext ctx)
    {
        if (state != State.Flying) return;
        if (spriteRenderer == null) { spriteRenderer = GetComponent<SpriteRenderer>(); }
        spriteRenderer.color = parryColor;
        state = State.Parrying;
        Invoke("ParryFinish",0.5f);
    }

    void ParryFinish()
    {
        state = State.Recovering;
        spriteRenderer.color = Color.white;
        Invoke("BackToNormal", 0.5f);
    }

    void BackToNormal()
    {
        state = State.Flying;
    }
    void ForceStop(InputAction.CallbackContext ctx)
    {
        state = State.Stopping;
        inputActions.Air.Disable();
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.angularVelocity = -3500;
        Invoke("Fall", 0.5f);
    }

    void Fall()
    {
        rb.angularVelocity = 0;
        rb.rotation = 0;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.velocity = Vector2.down*50;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Collision();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Invoke("Lose",1);
        }
    }

    private void Lose()
    {
        if (!GoalScript.won)
        {
            lost = true;
            losePanel.SetActive(true);
            input.Next.NextScene.performed += _ => SameScene();
            input.Next.NextScene.Enable();
        }
    }
    private void Collision()
    {
        if (state == State.Parrying)
        {
            audio.Play();
            rb.AddForce(new Vector2(1,1) * 250);
            return;
        }
        if (state == State.Flying || state == State.Recovering)
        {
            rb.AddForce((new Vector2(-50, 1)).normalized * 250);
        }
    }


    private void SameScene()
    {
        int cScene = SceneManager.GetActiveScene().buildIndex;
        if (cScene >= SceneManager.sceneCountInBuildSettings) { cScene = 0; }
        input.Next.NextScene.performed -= _ => SameScene();
        input.Next.NextScene.Disable();
        SceneManager.LoadScene(cScene);
    }
}
