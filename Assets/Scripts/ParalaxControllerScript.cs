using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] capas;
    [SerializeField] private ParalaxSprites spirtes;
    [SerializeField] private float speed;
    [Tooltip("Tiempo (en segundos) aprox. que tardar� en generar elementos en pantalla")]
    [SerializeField] private float timeGenerator;


    public void InitParalax()
    {

    }
}
