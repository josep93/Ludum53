using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyClarityScript : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField]Transform package;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        var alpha = (4000-package.position.x )/ 3000;
        var color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        sprite.color = color;
    }
}
