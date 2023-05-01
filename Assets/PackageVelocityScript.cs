using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PackageVelocityScript : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] TextMeshProUGUI velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        velocity.text = "Speed: " + String.Format("{0:.##}", rb.velocity.x);
    }
}
