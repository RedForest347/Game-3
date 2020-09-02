using RangerV;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//пока бесполезен
public class AudioCmp : ComponentBase, ICustomAwake
{
    //public event Action OnSta

    public AudioSource audioSource;
    public AudioClip OpenSound;
    public AudioClip CloseSound;

    public void OnAwake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /*
    void StartCloseSound()
    {
        audioSource.PlayOneShot(Close, 1);
    }

    void StartOpenSOund()
    {

    }*/
}
