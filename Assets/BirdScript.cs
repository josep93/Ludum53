using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField]Sprite[] sprites;
    SpriteRenderer sprite;
    int iterator=0;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>= 0.3f)
        {
            timer = 0;
            iterator++;
            iterator = iterator >= sprites.Length ? 0 : iterator;
            sprite.sprite = sprites[iterator];
        }
    }
}
