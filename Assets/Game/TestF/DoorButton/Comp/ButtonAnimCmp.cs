using RangerV;
using System;
using UnityEngine;

public class ButtonAnimCmp : ComponentBase, ICustomAwake
{
    public event Action<int> OnPress;
    public event Action<int> OnUnPress;

    public Animation anim;
    public GameObject doorHolder;

    public void OnAwake()
    {
        anim = GetComponent<Animation>();
    }

    public void Press()
    {
        OnPress?.Invoke(GetComponent<EntityBase>().entity);
    }

    public void UnPress()
    {
        OnUnPress?.Invoke(GetComponent<EntityBase>().entity);
    }
}
