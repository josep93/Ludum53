using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControllerScript : MonoBehaviour
{

    public static ObstacleControllerScript instance;

    [Header("Seguimiento paquete")]
    [SerializeField] private GameObject packageDelivery = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        //StartObstacle();
    }


    public void StartObstacle()
    {
        packageDelivery = GameObject.FindGameObjectWithTag("Package");
        GetComponentInChildren<ObstacleGeneratorScript>().StartObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (packageDelivery == null) { return; }
        transform.position = packageDelivery.transform.position;
    }
}
