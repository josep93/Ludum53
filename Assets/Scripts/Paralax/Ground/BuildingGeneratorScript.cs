using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGeneratorScript : MonoBehaviour
{

    [Header("Variables del paquete")]
    [SerializeField] private GameObject package;
    [SerializeField] private Rigidbody2D packageRb;
    [SerializeField] private float offset;

    [Header("Variables del generador")]
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject buildObject;
    [SerializeField] private float timeSpawn = 1;

    private Coroutine coroutine;
    private List<GameObject> builds = new();

    public static BuildingGeneratorScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //coroutine = StartCoroutine(GenerateBuild());
        packageRb = package.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (package.transform.position.y < 100 && coroutine == null)
        {
            coroutine = StartCoroutine (GenerateBuild());
        }*/

        transform.position = new Vector3(package.transform.position.x + offset, transform.position.y, transform.position.z);

        if (packageRb.velocity.x < 10)
        {
            StopGenerate();
            return;
        }

        if (package.transform.position.y < 100 && coroutine == null)
        {
            StartGenerate();
        }

    }

    public void StartGenerate()
    {
        if (coroutine != null) { return; }
        coroutine = StartCoroutine(GenerateBuild());
    }

    public void StopGenerate()
    {
        StopAllCoroutines();
        coroutine = null;
    }


    IEnumerator GenerateBuild()
    {
        while (true)
        {
            GameObject c = GetBuild();

            float paralaxEffect = Random.Range(0f, 0.3f);
            c.GetComponent<BuildObjectScript>().SetParalaxEffect(paralaxEffect);
            c.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
            c.GetComponent<SpriteRenderer>().sortingOrder = -3 - (int)(paralaxEffect * 10);

            yield return new WaitForSeconds(timeSpawn);
        }
    }


    private GameObject GetBuild()
    {
        foreach (var cBuild in builds)
        {
            if (!cBuild.activeInHierarchy)
            {
                cBuild.transform.position = transform.position;
                cBuild.SetActive(true);
                return cBuild;
            }
        }

        GameObject c = Instantiate(buildObject);
        c.transform.position = transform.position;
        builds.Add(c);
        return c;
    }

}