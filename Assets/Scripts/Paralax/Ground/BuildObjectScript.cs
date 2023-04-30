using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildObjectScript : MonoBehaviour
{
    [SerializeField] private float paralaxEffect;

    private GameObject cam;
    private float startPos;


    [SerializeField] float distanceCamera;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        startPos = transform.position.x;
        //Invoke(nameof(Deactivate), 5);
    }

    private void FixedUpdate()
    {
        float dist = cam.transform.position.x * paralaxEffect;
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        //distanceCamera = transform.position.x - cam.transform.position.x;
        //if (transform.position.x - cam.transform.position.x < -60) { Deactivate(); }
        //if (transform.position.x - package.transform.position.x > 100) { Deactivate(); }
    }

    private void OnDisable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }


    public void SetParalaxEffect(float paralaxEffect)
    {
        this.paralaxEffect = paralaxEffect;
    }


    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}