using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public static MusicScript current;
    AudioSource audioSource,audioSource2;
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioClip menuClip;

    // Start is called before the first frame update
    void Start()
    {
        if (current == null)
            current = this;
        if (current != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponents<AudioSource>()[0];
        audioSource2 = GetComponents<AudioSource>()[1];
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SelectMenu();
        }
    }

    public void SetMusicLevel(float level)
    {
        audioSource.volume = level;
        audioSource2.volume = level;
    }

    private void OnLevelWasLoaded(int level)
    {
        audioSource = GetComponents<AudioSource>()[0];
        audioSource2 = GetComponents<AudioSource>()[1];
        StopMusic();
    }

    public void StopMusic()
    {
        audioSource.Stop();
        audioSource2.Stop();
        audioSource.clip = null;
    }
    public void SelectTrack(int index, bool loop = false)
    {
        if (audioSource.clip == clips[index])return;
        audioSource.clip = clips[index];
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void SelectTrack_2(int index, bool loop = false)
    {
        if (audioSource2.clip == clips[index]) return;
        audioSource2.clip = clips[index];
        audioSource2.loop = loop;
        audioSource2.Play();
    }

    public void SelectMenu()
    {
        audioSource.clip = menuClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public float GetLengthTrack(int index)
    {
        return clips[index].length;
    }
}
