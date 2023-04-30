using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControllerScript : MonoBehaviour
{
    [Header("Seguimiento paquete")]
    [SerializeField] private GameObject packageDelivery;

    // Start is called before the first frame update
    void Start()
    {
        packageDelivery = PackageDeliveryScript.realPackage.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = packageDelivery.transform.position;
    }
}
