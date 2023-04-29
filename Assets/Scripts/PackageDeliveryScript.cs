using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageDeliveryScript : MonoBehaviour
{
    [SerializeField] private float force = 5;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowPackage(float angle)
    {
        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);
        rb.AddForce(dir * force, ForceMode2D.Impulse); 
    }

}
