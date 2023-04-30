using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript current;
    AudioSource audioSource;
    [SerializeField]AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void StopMusic()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }
    public void SelectTrack(int index, bool loop = false)
    {
        if (audioSource.clip == clips[index]) return;
        audioSource.clip = clips[index];
        audioSource.loop = loop;
        audioSource.Play();
    }
}
