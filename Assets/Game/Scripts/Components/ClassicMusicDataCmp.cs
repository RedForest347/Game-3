using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;
using System;

public class ClassicMusicDataCmp : ComponentBase, ICustomAwake
{
    [HideInInspector]
    public bool music_started;

    public AudioClip audioClip;
    public AudioSource audioSource;

    public event Action<int> triggerEnter;

    public void OnAwake()
    {
        audioSource = GetComponent<AudioSource>();
        music_started = false;
    }

    //public int 




    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EntityBase>() != null)
        {
            triggerEnter?.Invoke(other.GetComponent<EntityBase>().entity);
        }
    }
}
