using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCmp : ComponentBase, ICustomAwake
{
    public AudioSource audioSource;
    public AudioClip Open;
    public AudioClip Close;

    public void OnAwake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void StartCloseSound()
    {
        //audioSource.Play();
        //audioSource.PlayOneShot(Open, 1);
        audioSource.PlayOneShot(Close, 1);

    }

    void StartOpenSOund()
    {

    }
}
