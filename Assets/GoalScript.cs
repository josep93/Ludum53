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
    [SerializeField] GameObject winScreen,winStamp, breakingIn;
    public static bool won = false;
    AudioSource audio;
    [SerializeField]AudioClip stamp;

    private InputSystem input;

    // Start is called before the first frame update
    void Start()
    {
        won = false;
        audio = GetComponent<AudioSource>();
        input = new();
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

            if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
            {
                if (distance_int > 1000)
                {
                    distance.color = Color.white;
                }
                if (distance_int < 1000)
                {
                    distance.color = Color.yellow;
                }
                if (distance_int < 15)
                {
                    distance.color = Color.green;
                }
                if (distance_int < -15)
                {
                    distance.color = Color.red;
                }
            }

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
        if (!AirPackageScript.lost)
        {
            won = true;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                BreakIn();
                Invoke("StopObject", 0.05f);
            }
            else if (ultraHigh)
            {
                audio.Play();
                breakingIn.SetActive(true);
                StopObject();
            }
            else
            {

                BreakInHorizontal();
            }
            Invoke("WinScreen", 0.3f);
            Invoke("WinStamp", 1.3f);
        }
    }

    void BreakIn()
    {
        breakingIn.SetActive(true);
        breakingIn.transform.position = new Vector3(breakingIn.transform.position.x, PackageDeliveryScript.realPackage.transform.position.y, breakingIn.transform.position.z);
        audio.Play();
    }

    void BreakInHorizontal()
    {
        breakingIn.SetActive(true);
        breakingIn.transform.position = new Vector3(PackageDeliveryScript.realPackage.transform.position.x, breakingIn.transform.position.y, breakingIn.transform.position.z);
        audio.Play();
        PackageDeliveryScript.realPackage.gameObject.SetActive(false);
    }

    void StopObject()
    {
        PackageDeliveryScript.realPackage.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        PackageDeliveryScript.realPackage.rb.rotation = 0;
        foreach (AudioSource audio in PackageSoundScript.current.audio)
        {
            audio.enabled = false;
        }
    }

    void WinScreen()
    {
        winScreen.SetActive(true);
    }

    void WinStamp()
    {
        winStamp.SetActive(true);
        audio.clip = stamp;
        audio.Play();
        
        input.Next.NextScene.performed += _ => NextScene();
        input.Next.NextScene.Enable();
    }

    private void NextScene()
    {
        int cScene = SceneManager.GetActiveScene().buildIndex;
        cScene++;
        if (cScene >= SceneManager.sceneCountInBuildSettings) { cScene = 0; }
        input.Next.NextScene.performed -= _ => NextScene();
        input.Next.NextScene.Disable();
        SceneManager.LoadScene(cScene);
    }

}
