using RangerV;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimCmp : ComponentBase, ICustomAwake
{
    public bool is_open;
    public bool is_close;

    public event Action<int> OnStartOpen;
    public event Action<int> OnCompleteOpen;
    public event Action<int> OnStartClose;
    public event Action<int> OnCompleteClose;

    public Animation anim;

    public void OnAwake()
    {
        anim = GetComponent<Animation>();
    }

    public void StartOpen()
    {
        int ent = GetComponent<EntityBase>().entity;
        OnStartOpen?.Invoke(ent);
    }

    public void Open()
    {
        int ent = GetComponent<EntityBase>().entity;
        OnCompleteOpen?.Invoke(ent);
    }

    public void StartClose()
    {
        int ent = GetComponent<EntityBase>().entity;
        OnStartClose?.Invoke(ent);
    }

    public void Close()
    {
        int ent = GetComponent<EntityBase>().entity;
        OnCompleteClose?.Invoke(ent);
    }
}
