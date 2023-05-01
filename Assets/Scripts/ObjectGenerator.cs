using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject objectToGenerate;
    public float generateInterval = 1.0f;
    public int maxObjects = 10;
    public GameObject followObject;
    public float generatorSpeed = 10.0f;

    private int generatedObjects = 0;
    private float timeSinceLastGeneration = 0.0f;
    private float followObjectYOffset;

    public float XOffset;

    private static ObjectGenerator instance;

    //public static ObjectGenerator Instance
    //{
    ////    get
    ////    {
    ////        if (instance == null)
    ////        {
    ////            instance = FindObjectOfType<ObjectGenerator>();
    ////        }
    ////        return instance;
    ////    }
    //}

    private void Start()
    {
        //if (followObject != null)
        //{
        //    followObjectYOffset = transform.position.y - followObject.transform.position.y;
        //}
    }

    void Update()
    {
        timeSinceLastGeneration += Time.deltaTime;

        if (generatedObjects < maxObjects && timeSinceLastGeneration >= generateInterval)
        {
            GenerateObject();
            generatedObjects++;
            timeSinceLastGeneration = 0.0f;
        }
        transform.position = new Vector3(followObject.transform.position.x + XOffset, followObject.transform.position.y, transform.position.z);


    }

    void GenerateObject()
    {
        //Vector3 position = new Vector3(transform.localPosition.x + 10.0f, Random.Range(0.0f, 5.0f), Random.Range(-10.0f, 10.0f));
        //GameObject obj = Instantiate(objectToGenerate, position, Quaternion.identity, transform);
        //obj.transform.parent = transform;
        //ObjectLifetime lifetime = obj.AddComponent<ObjectLifetime>();
        //lifetime.Lifetime = 10.0f;
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(-5.0f, 0.0f);
        GameObject gameobject = Instantiate(objectToGenerate, transform);
        Rigidbody2D rb = gameobject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-5.0f, 0.0f);
    }
}

