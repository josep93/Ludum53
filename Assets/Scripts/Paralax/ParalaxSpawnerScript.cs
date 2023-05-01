using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject packageDelivery;
    [SerializeField] private float distance;

    private Rigidbody2D rbPackage;

    // Start is called before the first frame update
    void Start()
    {
        if (packageDelivery == null)
        {
            packageDelivery = GetComponentInParent<ParalaxControllerScript>().GetPackageDelivery();
        }
        rbPackage = packageDelivery.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rbPackage.velocity.magnitude < 0.5f) {
            transform.localPosition = Vector2.right * distance;
            return;
        }
        transform.localPosition = rbPackage.velocity.normalized * distance;
    }
}
