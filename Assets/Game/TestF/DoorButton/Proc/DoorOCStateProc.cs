﻿using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOCStateProc : ProcessingBase, ICustomUpdate, ICustomAwake, ICustomDisable
{
    Group DoorGroup = Group.Create(new ComponentsList<DoorAnimCmp>());

    public void OnAwake()
    {
        DoorGroup.OnAddEntity += OnAddEnt;
        DoorGroup.OnBeforeRemoveEntity += OnRemoveEnt;

    }

    public void OnAddEnt(int door)
    {
        Storage.GetComponent<DoorAnimCmp>(door).OnStartClose += (int ent) => Storage.GetComponent<DoorAnimCmp>(ent).is_open = false;
        Storage.GetComponent<DoorAnimCmp>(door).OnStartOpen += (int ent) => Storage.GetComponent<DoorAnimCmp>(ent).is_close = false;
        Storage.GetComponent<DoorAnimCmp>(door).OnCompleteOpen += (int ent) => Storage.GetComponent<DoorAnimCmp>(ent).is_open = true;
        Storage.GetComponent<DoorAnimCmp>(door).OnCompleteClose += (int ent) => Storage.GetComponent<DoorAnimCmp>(ent).is_close = true;
    }

    public void OnRemoveEnt(int door)
    {
        Storage.GetComponent<DoorAnimCmp>(door).OnStartClose -= (int ent) => Storage.GetComponent<DoorAnimCmp>(ent).is_open = false;
        Storage.GetComponent<DoorAnimCmp>(door).OnStartOpen -= (int ent) => Storage.GetComponent<DoorAnimCmp>(ent).is_close = false;
        Storage.GetComponent<DoorAnimCmp>(door).OnCompleteOpen -= (int ent) => Storage.GetComponent<DoorAnimCmp>(ent).is_open = true;
        Storage.GetComponent<DoorAnimCmp>(door).OnCompleteClose -= (int ent) => Storage.GetComponent<DoorAnimCmp>(ent).is_close = true;
    }

   

    public void CustomUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public void OnCustomDisable()
    {
        DoorGroup.OnAddEntity -= OnAddEnt;
        DoorGroup.OnAfterRemoveEntity -= OnRemoveEnt;
    }
}
