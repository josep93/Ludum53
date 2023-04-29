using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapaController : MonoBehaviour
{
    [SerializeField] private GameObject paralaxObject;

    [Tooltip("Porcentaje de la escala original del sprite (0 - 100)")]
    [SerializeField] private float scaleReduction;
    [Tooltip("Porcentaje de la velocidad original (0 - 100)")]
    [SerializeField] private float speedReduction;

    private Sprite[] sprites;
    private float speed;
    private float timeGenerator;

    private void Start()
    {
        if (paralaxObject == null)
        {
            paralaxObject = Resources.Load<GameObject>("Prefabs/ParalaxObject");
        }
    }


    public void InitCapa(Sprite[] sprites, float speed, float timeGenerator)
    {
        this.sprites = sprites;
        this.speed = speed * (speedReduction / 100);
        this.timeGenerator = timeGenerator;

    }


    IEnumerator GenrateObject()
    {
        while (true)
        {
            yield return null;
        }
    }
}
