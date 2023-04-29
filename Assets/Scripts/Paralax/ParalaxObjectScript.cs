using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxObjectScript : MonoBehaviour
{
    private float speed = 0;
    private GameObject target;

    private void Start()
    {
        target = GameObject.FindWithTag("Finish");
        if (target == null)
        {
            target = GameObject.Find("ParalaxDestroyer");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position, 
            new Vector2(target.transform.position.x, transform.position.y), 
            speed * Time.deltaTime);
    }


    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
