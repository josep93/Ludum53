using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorCamara : MonoBehaviour
{

    private Camera camara;
    public Color colorInicio;
    public Color colorFinal;
    public float duration = 2.0f;
    public float xThreshold = 5.0f;
    private float startTime;
    private float startX;
    private float valor = 0;
    private float valorT;
    public Vector3 goalPosition;
    public static BackgroundColorCamara instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }
    private void Start()
    {
        float x = transform.position.x;

    }
   
    public void ChangeColorCamera()
    {
            camara = GetComponent<Camera>();
            colorInicio = camara.backgroundColor;
            startTime = Time.time; 
            startX = transform.position.x; 
            goalPosition = GameObject.Find("Goal").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //float t = 0.0f; 

        //if (Mathf.Abs(transform.position.x - startX) >= xThreshold) 
        //{
        //    t = (Time.time - startTime) / duration; 
        //    t = Mathf.Clamp01(t); 
        //}

        camara.backgroundColor = Color.Lerp(colorInicio, colorFinal, valor); 
        valor += 0.01f;

        float x = 0;
        valorT = valor * (goalPosition.x / x);


    }
}
