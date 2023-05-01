using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField]Sprite[] sprites;
    [SerializeField] private bool tutorial = false;
    [Tooltip("Velocidad de seguimiento en % del paquete")]
    [SerializeField] private float speedFollow = 30;

    private Rigidbody2D packageRb;
    private Rigidbody2D rb;
    private AudioSource audio;

    SpriteRenderer sprite;
    int iterator=0;
    float timer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();


        if (tutorial) { return; }
        packageRb = GameObject.FindGameObjectWithTag("Package").GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
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

        if (packageRb == null) { return; }
        rb.velocity = new Vector2(packageRb.velocity.x * (speedFollow / 100), packageRb.velocity.y * (speedFollow / 100));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Package"))
        {
            if (audio.isPlaying) { return; }
            audio.Play();
        }
    }

}
