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
    [Tooltip("Offset de generación de los objetos a partir del punto de origen")]
    [SerializeField] private Vector2 offsetPosition = new(5, 5);
    [Tooltip("Offset de tiempo de generación de los objetos")]
    [SerializeField] private float offsetTimeGenerator = 1f;

    private Sprite[] sprites;
    private float speed;
    private float timeGenerator;
    private Coroutine coroutine;

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
        coroutine = StartCoroutine(GenerateObject());
    }

    public void StopCapa()
    {
        if (coroutine == null) { return; }
        StopCoroutine(coroutine);
    }

    public void SetOffset(Vector2 offset)
    {
        this.offsetPosition = offset;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed * (speedModification / 100);
    }

    public void SetSprites(Sprite[] sprites)
    {
        this.sprites = sprites;
    }

    public void SetTimeGenerator(float timeGenerator)
    {
        this.timeGenerator = timeGenerator;
    }

    public float GetModificationSpeed()
    {
        return speed;
    }

    IEnumerator GenerateObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeGenerator + Random.Range(-offsetTimeGenerator, offsetTimeGenerator));
            
            GameObject c = paralaxPool.GetObject();
            c.transform.position = new Vector2(
                paralaxSpawner.transform.position.x + Random.Range(-offsetPosition.x, offsetPosition.x), 
                paralaxSpawner.transform.position.y + Random.Range(-offsetPosition.y, offsetPosition.y));
            c.transform.localScale = c.transform.localScale * (scaleModification / 100);

            ParalaxObjectScript paralaxObjectScript = c.GetComponent<ParalaxObjectScript>();
            paralaxObjectScript.SetSpeed(speed);
            paralaxObjectScript.SetController(this);

            SpriteRenderer cs = c.GetComponent<SpriteRenderer>();
            cs.sprite = sprites[Random.Range(0, sprites.Length)];
            cs.sortingOrder = layer;
        }
    }
}
