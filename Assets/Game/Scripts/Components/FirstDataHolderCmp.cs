using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDataHolderCmp : ComponentBase, ICustomAwake
{
    public AudioClip firstSteps;
    public AudioClip pedestalSound;
    public AudioSource audioSource;

    public void OnAwake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
