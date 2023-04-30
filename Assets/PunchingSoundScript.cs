using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingSoundScript : MonoBehaviour
{
    [SerializeField] AudioClip[] punchSounds,shoutSounds;
    [SerializeField] AudioClip lastPunchSound,brokenMug;
    AudioSource[] audios;
    int iterator=0;
    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPunch()
    {
        var audio = audios[iterator];
        var selectedAudio = Random.Range(0, punchSounds.Length);
        audio.clip = punchSounds[selectedAudio];
        audio.Play();
        iterator++;
        iterator = iterator > punchSounds.Length-2 ? 0 : iterator;
    }

    public void CallShout()
    {
        var audio = audios[iterator];
        var selectedAudio = Random.Range(1, punchSounds.Length);
        audio.clip = shoutSounds[selectedAudio];
        audio.Play();
        iterator++;
        iterator = iterator > punchSounds.Length - 2 ? 0 : iterator;
    }

    public void Shout()
    {
        var audio = audios[iterator];
        audio.clip = shoutSounds[0];
        audio.Play();
        iterator++;
        iterator = iterator > shoutSounds.Length - 2 ? 0 : iterator;
    }

    public void BreakMug()
    {
        var audio = audios[iterator];
        audio.clip = brokenMug;
        audio.Play();
        iterator++;
        iterator = iterator > shoutSounds.Length - 2 ? 0 : iterator;
    }

    public void FinalPunch()
    {
        foreach (AudioSource audioInstance in audios){
            audioInstance.Stop();
        }
        var audio = audios[0];
        audio.clip = lastPunchSound;
        audio.Play();
    }

    public void FinalShout()
    {
        var audio = audios[1];
        audio.clip = shoutSounds[0];
        audio.Play();
    }
}
