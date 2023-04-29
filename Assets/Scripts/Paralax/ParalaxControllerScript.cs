using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] capas;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float speed;
    [Tooltip("Tiempo (en segundos) aprox. que tarda en generar elementos en pantalla")]
    [SerializeField] private float timeGenerator;


    private void Start()
    {
        InitParalax();
    }

    public void InitParalax()
    {
        foreach (var cap in capas)
        {
            CapaController cc = cap.GetComponent<CapaController>();
            cc.InitCapa(sprites, speed, timeGenerator);
        }
    }


    public void SetSpeed(float speed)
    {
        foreach (var cap in capas)
        {
            CapaController cc = cap.GetComponent<CapaController>();
            cc.SetSpeed(speed);
        }
    }

    public void SetDirectio(Vector2 direction)
    {
        
    }
}
