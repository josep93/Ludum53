using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifetime : MonoBehaviour
{
    public float Lifetime { get; set; }
    private float currentLifetime = 0.0f;

    private void Update()
    {
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= Lifetime)
        {
            Destroy(gameObject);
        }
    }
}