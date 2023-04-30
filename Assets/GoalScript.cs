using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GoalScript : MonoBehaviour
{
    [SerializeField] bool ultraHigh;
    [SerializeField] TextMeshProUGUI distance;
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
            distance.text = "Distance: " + String.Format("{0:.##}", (transform.position.x - PackageDeliveryScript.realPackage.transform.position.x));
        }
    }

    void Win()
    {
        Debug.Log("Win");
    }
}
