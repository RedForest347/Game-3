using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class PlayerStepsCmp : ComponentBase, ICustomAwake
{
    [HideInInspector]
    public AudioSource audioSource;

    public void OnAwake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
