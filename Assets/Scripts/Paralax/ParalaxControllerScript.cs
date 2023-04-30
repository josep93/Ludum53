using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParalaxControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] capas;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float speed;
    [Tooltip("Tiempo (en segundos) aprox. que tarda en generar elementos en pantalla")]
    [SerializeField] private float timeGenerator;

    [Header("Seguimiento paquete")]
    [SerializeField] private GameObject packageDelivery;
    [SerializeField] private bool isWorking = false;
    [Tooltip("Aplica movimiento vertical dependiente de del movimiento del paquete")]
    [SerializeField] private bool hasVerticalMove = true;
    [Tooltip("Altura mínima para que se active el Paralax")]
    [SerializeField] private float alturaMinima;
    [Tooltip("Altura máxima para que se active el Paralax")]
    [SerializeField] private float alturaMaxima;

    private void Start()
    {
        //InitParalax();
    }

    private void Update()
    {

        if (hasVerticalMove)
        {
            transform.position = packageDelivery.transform.position;
        }
        else
        {
            // Aplicar solo movimiento horizontal
            transform.position = new Vector2(packageDelivery.transform.position.x, transform.position.y);
        }

        if (!packageDelivery.activeInHierarchy)
        {
            return;
        }

        float yPosition = packageDelivery.transform.position.y;

        if (isWorking)
        {
            if (yPosition < alturaMinima)
            {
                isWorking = false;
                StopParalax();
            }

            if (yPosition > alturaMaxima)
            {
                isWorking = false;
                StopParalax();
            }
            return;
        }

        if (yPosition > alturaMinima && yPosition < alturaMaxima)
        {
            isWorking = true;
            InitParalax();
        }
    }

    public void InitParalax()
    {
        foreach (var cap in capas)
        {
            CapaController cc = cap.GetComponent<CapaController>();
            cc.InitCapa(sprites, speed, timeGenerator, hasVerticalMove);
        }
    }

    public void StopParalax()
    {
        foreach (var cap in capas)
        {
            CapaController cc = cap.GetComponent<CapaController>();
            cc.StopCapa();
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


    public void SetOffset(Vector2 offset)
    {
        foreach (var cap in capas)
        {
            CapaController cc = cap.GetComponent<CapaController>();
            cc.SetOffset(offset);
        }
    }

    public void SetSprite(Sprite[] sprites)
    {
        foreach (var cap in capas)
        {
            CapaController cc = cap.GetComponent<CapaController>();
            cc.SetSprites(sprites);
        }
    }

    public GameObject GetPackageDelivery()
    {
        return packageDelivery;
    }
}
