using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PackageVelocityScript : MonoBehaviour
{

    public enum State : byte
    {
        Flying,
        Delivering
    }
    public State state = State.Flying;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    [SerializeField] TextMeshProUGUI velocityText;
    [SerializeField] GameObject fakePackage;
    SimulateGravityScript simulateGravity;
    byte flag = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        velocityText.gameObject.SetActive(true);
        simulateGravity = GetComponent<SimulateGravityScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Flying)
        {
            if (flag == 0)
            {
                velocityText.text = "Speed: " + String.Format("{0:.##}", rb.velocity.x);
            }
            else if (flag == 1)
            {
                velocityText.text = "Speed: 0.00";
            }
            else
            {
                velocityText.text = "Speed: Enough";
            }
            if (rb.velocity.x < 0)
            {
                ChangeState(State.Delivering);
            }
        }
    }

    private void ChangeState(State state)
    {
        flag = 1;
        DelivererScript.current.transform.position = new Vector2(transform.position.x - 3.18134f, transform.position.y);
        PackageScript.current.transform.position = new Vector2(transform.position.x, transform.position.y);
        DelivererScript.current.musicFlag = true;
        DelivererScript.current.StateUpdate(DelivererScript.State.Prepared);
        fakePackage.SetActive(true);
        PackageScript.current.StateChange(PackageScript.State.Ready);
        PackageScript.current.sprite.enabled = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        simulateGravity.enabled = false;
        Invoke("EnoughSpeed", 1f);
    }

    void EnoughSpeed()
    {
        flag = 2;
    }
}
