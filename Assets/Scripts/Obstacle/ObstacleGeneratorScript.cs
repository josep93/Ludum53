using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleGeneratorScript : MonoBehaviour
{
    [Header("Propiedades del generador")]
    [SerializeField] private GameObject[] obstaclesAir;
    [SerializeField] private GameObject[] obstaclesGround;
    [SerializeField] private float distance = 13;
    [SerializeField] private float timeGenerator = 5;
    [SerializeField] private Transform obstacleGroundGenerator;

    [Header("Paquete")]
    [SerializeField] private GameObject packageDelivery = null;
    private Rigidbody2D rbPackage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartObstacle()
    {
        if (packageDelivery == null)
        {
            packageDelivery = GameObject.FindGameObjectWithTag("Package");
        }

        rbPackage = packageDelivery.GetComponent<Rigidbody2D>();
        StartCoroutine(GenerateObstacle());
    }


    // Update is called once per frame
    void Update()
    {
        if (rbPackage == null) { return; }
        transform.localPosition = rbPackage.velocity.normalized * distance;
        if(rbPackage.velocity.x < 5)
        {
            StopAllCoroutines();
        }
    }


    IEnumerator GenerateObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeGenerator + Random.Range(-1f, 1f));

            GameObject c;

            // Esta cerca del suelo
            if (packageDelivery.transform.position.y < 50)
            {
                c = Instantiate(obstaclesGround[Random.Range(0, obstaclesGround.Length)]);
                c.transform.position = transform.position;
                continue;
            }

            c = Instantiate(obstaclesAir[Random.Range(0, obstaclesAir.Length)]);
            c.transform.position = transform.position;
            // Está en el aire

        }
    }
}
