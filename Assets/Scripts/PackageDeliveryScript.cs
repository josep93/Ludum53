using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageDeliveryScript : MonoBehaviour
{
    [SerializeField] private float force = 5;
    [SerializeField] private float angle = 45;

    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ThrowPackage(float force = 0, float angle = 0)
    {
        if (angle == 0)
        {
            angle = this.angle;
        }
        if (force == 0)
        {
            force = this.force;
        }

        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);
        rb.AddForce(dir * force, ForceMode2D.Impulse); 
    }

}
