using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxObjectScript : MonoBehaviour
{
    [SerializeField] private float lifeTime = 8;
    private bool hasVerticalMove = false;

    private GameObject spawner;
    private CapaController controller;

    private Vector3 origin;
    private Vector3 direction;
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawner = GameObject.FindGameObjectWithTag("Respawn");
        origin = GameObject.Find("ParalaxController").transform.position;
        StartCoroutine(DisableObject());
    }

    
    void Update()
    {
        direction = (origin - spawner.transform.position).normalized;

        if (!hasVerticalMove)
        {
            direction = new Vector3(direction.x, 0);
        }        
        rb.velocity = direction * controller.GetModificationSpeed();
    }


    public void SetController(CapaController controller)
    {
        this.controller = controller;
    }

    public void SetVerticalMove(bool hasVerticalMove)
    {
        this.hasVerticalMove = hasVerticalMove;
    }


    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(lifeTime);
        this.gameObject.SetActive(false);
    }

}
