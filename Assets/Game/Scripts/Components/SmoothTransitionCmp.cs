using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class SmoothTransitionCmp : ComponentBase, ICustomAwake
{
    public event Action screenDarkening;
    public event Action screenUndarkening;

    [HideInInspector]
    public Animation anim;


    public void OnAwake()
    {
        anim = GetComponent<Animation>();
    }



    public void OnScreenDarkening()
    {
        screenDarkening?.Invoke();
    }

    public void OnScreenUndarkening()
    {
        screenUndarkening?.Invoke();
    }

}
