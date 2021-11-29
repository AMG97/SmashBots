using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Sounds : MonoBehaviour
{
    public AudioClip audioData_Adv;
    public AudioClip audioData_Back;
    AudioSource audio;

    private void Start() {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound_Advance()
    {
        audio.clip = audioData_Adv;
        audio.Play();
    }
    public void PlaySound_Back()
    {
        audio.clip = audioData_Back;
        audio.Play();
    }
}
