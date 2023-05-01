using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    [SerializeField] bool ultraHigh;
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] GameObject winScreen, breakingIn;
    bool won = false;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PackageDeliveryScript.realPackage == null) return;
        SetDistance();
        if (won) return;
        if (ultraHigh)
        {
            if (PackageDeliveryScript.realPackage.transform.position.x >= transform.position.x) Win();
        }
    }

    void SetDistance()
    {
        if (distance.IsActive())
        {
            var distance_int = transform.position.x - PackageDeliveryScript.realPackage.transform.position.x;
            if (ultraHigh) distance.text = "Distance: " + String.Format("{0:.##}", distance_int < 0 ? "0.00" : distance_int);
            else distance.text = "Distance: " + String.Format("{0:.##}", distance_int);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ultraHigh) return;
        if (collision.tag == "Package")
        {
            Win();
        }
    }

    void Win()
    {
        won = true;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            BreakIn();
            Invoke("StopObject", 0.05f);
        }
        Invoke("WinScreen", 0.3f);
    }

    void BreakIn()
    {
        breakingIn.SetActive(true);
        breakingIn.transform.position = new Vector3(breakingIn.transform.position.x, PackageDeliveryScript.realPackage.transform.position.y, breakingIn.transform.position.z);
        audio.Play();
    }
    void StopObject()
    {
        PackageDeliveryScript.realPackage.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        foreach (AudioSource audio in PackageSoundScript.current.audio)
        {
            audio.enabled = false;
        }
    }

    void WinScreen()
    {
        winScreen.SetActive(true);
    }
}
