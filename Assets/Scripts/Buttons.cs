using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour
{
 public AudioSource audioSource;
    //public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
     //  audioSource.clip = audioClip;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBotonEmpezar()
    {
        
     //   audioSource.Play();
        SceneManager.LoadScene(1);    

    }

    public void OnPointerEnter()
    {
    //audioSource.clip = sound;
    audioSource.Play();
    }

    public void OnPointerExit()
    {
    audioSource.Stop();
    }
}


