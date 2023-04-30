using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GoalScript : MonoBehaviour
{
    [SerializeField] bool ultraHigh;
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PackageDeliveryScript.realPackage == null) return;
        SetDistance();
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
            distance.text = "Distance: " + String.Format("{0:.##}", distance_int<0?"0.00":distance_int);
        }
    }

    void Win()
    {
        Invoke("TimeStop", 0.05f);
        Invoke("WinScreen", 0.3f);
    }

    void TimeStop()
    {
        PackageDeliveryScript.realPackage.rb.velocity = Vector3.zero;
        PackageDeliveryScript.realPackage.rb.angularVelocity = 0;
    }

    void WinScreen()
    {
        winScreen.SetActive(true);
    }
}
