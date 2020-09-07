using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlesCmp : ComponentBase, ICustomAwake
{
    public Animation anim;

    public void OnAwake()
    {
        anim = GetComponent<Animation>();
    }
}
