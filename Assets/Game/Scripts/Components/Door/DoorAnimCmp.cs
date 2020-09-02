using RangerV;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Component("Door/DoorAnimCmp")]
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
        //Debug.Log("StartOpen");
        int ent = GetComponent<EntityBase>().entity;
        OnStartOpen?.Invoke(ent);
    }

    public void EndOpen()
    {
        //Debug.Log("EndOpen");
        int ent = GetComponent<EntityBase>().entity;
        OnCompleteOpen?.Invoke(ent);
    }

    public void StartClose()
    {
        //Debug.Log("StartClose");
        int ent = GetComponent<EntityBase>().entity;
        OnStartClose?.Invoke(ent);
    }

    public void EndClose()
    {
        //Debug.Log("EndClose");
        int ent = GetComponent<EntityBase>().entity;
        OnCompleteClose?.Invoke(ent);
    }
}
