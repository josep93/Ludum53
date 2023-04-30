using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScenary : MonoBehaviour
{
    [SerializeField] private Transform package;
    [SerializeField] private Transform controlInit;
    [SerializeField] private Transform controlEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (package.position.x >= controlEnd.position.x)
        {
            transform.position = new Vector2(package.position.x + controlInit.localPosition.x, transform.position.y);
        }

    }
}
