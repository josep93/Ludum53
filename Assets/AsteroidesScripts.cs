using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidesScripts : MonoBehaviour
{
    public Sprite[] sprites; // Un arreglo que contiene los sprites que quieres utilizar

    void Start()
    {
        int randomIndex = Random.Range(0, sprites.Length); // Selecciona un índice aleatorio en el rango de 0 y la longitud del arreglo de sprites
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene el componente SpriteRenderer del objeto

        if (spriteRenderer != null) // Verifica que el componente SpriteRenderer exista en el objeto
        {
            spriteRenderer.sprite = sprites[randomIndex]; // Asigna el sprite seleccionado al objeto
        }
    }
}
