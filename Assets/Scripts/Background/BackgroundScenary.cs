using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScenary : MonoBehaviour
{
    [SerializeField] private Transform package;
    [SerializeField] private bool yFreeze;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (yFreeze)
        {
            transform.position = new Vector3(package.position.x, transform.position.y);
            return;
        }

        transform.position = package.position;
    }
}
