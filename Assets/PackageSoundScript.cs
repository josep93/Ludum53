using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSoundScript : MonoBehaviour
{
    public AudioSource[] audio;

    public static PackageSoundScript current;

    private void Start()
    {
        current = this;
        audio = GetComponents<AudioSource>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            StartCollision();
            StartRoll();
        }


        if (collision.gameObject.name.Equals("StupidCar"))
        {
            if (audio[2].isPlaying) return;
            audio[2].Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            StopRoll();
        }
    }

    void StartCollision()
    {
        if (audio[0].isPlaying) return;
        audio[0].Play();
    }

    void StartRoll()
    {
        if (audio[1].isPlaying) return;
        audio[1].Play();
    }

    void StopRoll()
    {
        audio[1].Stop();
    }
}
