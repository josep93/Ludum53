using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorCamara : MonoBehaviour
{

    private Camera camara;
    private float halfGoal;
    private float valor = 0; 

    public Color colorInicio;
    public Color colorFinal;
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
        
    }
   
    public void ChangeColorCamera()
    {
        halfGoal = goalPosition.x / 2;
        camara = GetComponent<Camera>();
        colorInicio = camara.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (camara == null) { return; }
        
        camara.backgroundColor = Color.Lerp(colorInicio, colorFinal, valor);
        valor += 0.0015f;
    }
}
