using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Sounds : MonoBehaviour
{
    public AudioClip audioData_Adv;
    public AudioClip audioData_Back;
    public new AudioSource audio;

    public void PlaySound_Advance()
    {
        audio.Stop();
        audio.clip = audioData_Adv;
        audio.Play();
    }
    public void PlaySound_Back()
    {
        audio.Stop();
        audio.clip = audioData_Back;
        audio.Play();
    }
}
