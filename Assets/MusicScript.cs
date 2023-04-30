using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public static MusicScript current;
    AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SelectMenu();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        audioSource = GetComponent<AudioSource>();
        StopMusic();
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
