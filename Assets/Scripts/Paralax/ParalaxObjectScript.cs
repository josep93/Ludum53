using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxObjectScript : MonoBehaviour
{
    [SerializeField] private float lifeTime = 8;

    private GameObject spawner;
    private CapaController controller;

    private Vector3 origin;
    private Vector3 direction;
    private float speed = 3;
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawner = GameObject.FindGameObjectWithTag("Respawn");
        origin = Vector3.zero;
        StartCoroutine(DisableObject());
    }

    
    void Update()
    {
        direction = (origin - spawner.transform.position).normalized;
        rb.velocity = direction * controller.GetModificationSpeed();
    }


    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetController(CapaController controller)
    {
        this.controller = controller;
    }


    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(lifeTime);
        this.gameObject.SetActive(false);
    }

}
