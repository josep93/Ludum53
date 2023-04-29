using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapaController : MonoBehaviour
{
    [SerializeField] private GameObject paralaxObject;
    [SerializeField] private GameObject paralaxSpawner;
    [SerializeField] private ParalaxObjectPooling paralaxPool;

    [Tooltip("Porcentaje de la escala original del sprites (0% - ...)")]
    [SerializeField] private float scaleModification = 100;
    [Tooltip("Porcentaje de la velocidad original (0% - ...)")]
    [SerializeField] private float speedModification = 100;
    [Tooltip("Orden de posición en el layer (Por defecto 0)")]
    [SerializeField] private int layer = 0;
    [Tooltip("Rango de coordenadas Y que generará el objeto (ej: 5 -> desde -5y a 5y)")]
    [SerializeField] private float heightLimit = 5;

    private Sprite[] sprites;
    private float speed;
    private float timeGenerator;

    private void Start()
    {
        if (paralaxObject == null)
        {
            paralaxObject = Resources.Load<GameObject>("Prefabs/ParalaxObject");
        }
        paralaxPool = GetComponentInParent<ParalaxObjectPooling>();
    }


    public void InitCapa(Sprite[] sprites, float speed, float timeGenerator)
    {
        this.sprites = sprites;
        this.speed = speed * (speedModification / 100);
        this.timeGenerator = timeGenerator;
        StartCoroutine(GenerateObject());
    }


    IEnumerator GenerateObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeGenerator + Random.Range(-1f, 1f));
            
            GameObject c = paralaxPool.GetObject();
            c.transform.position = new Vector2(
                paralaxSpawner.transform.position.x, 
                paralaxSpawner.transform.position.y + Random.Range(-heightLimit, heightLimit));
            c.transform.localScale = c.transform.localScale * (scaleModification / 100);
            c.GetComponent<ParalaxObjectScript>().SetSpeed(speed);

            SpriteRenderer cs = c.GetComponent<SpriteRenderer>();
            cs.sprite = sprites[Random.Range(0, sprites.Length)];
            cs.sortingOrder = layer;
        }
    }
}
