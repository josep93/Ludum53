using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalsScript : MonoBehaviour
{
    Rigidbody2D[] rbs;
    // Start is called before the first frame update
    void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rb in rbs)
        {
            Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, 180f + Random.Range(-30f, 30f)) * Vector2.right);
            rb.AddForce(dir * 10, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-400f, 400f));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
