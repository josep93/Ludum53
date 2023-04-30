using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriesScript : MonoBehaviour
{
    Rigidbody2D[] rbs;
    // Start is called before the first frame update
    void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rb in rbs)
        {
            Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, 20f + Random.Range(-5f, 5f)) * Vector2.right);
            rb.AddForce(dir * 70, ForceMode2D.Impulse);
            rb.AddTorque(360f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
