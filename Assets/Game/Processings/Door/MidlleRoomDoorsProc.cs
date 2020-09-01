using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class MidlleRoomDoorsProc : ProcessingBase, ICustomUpdate, ICustomAwake, ICustomDisable
{
    //Group MidlleRoomDoorsGroup = Group.Create(new ComponentsList<>());
    Group TriggerGroup = Group.Create(new ComponentsList<LastRommTriggerCmp>());
    Group MidlleRoomDataGroup = Group.Create(new ComponentsList<MidlleRoomDataCmp>());


    public void OnAwake()
    {
        foreach (int data in TriggerGroup)
            Storage.GetComponent<LastRommTriggerCmp>(data).onTriggetEnter += TriggerEnter;

        TriggerGroup.OnAddEntity += OnAdd;
        TriggerGroup.OnBeforeRemoveEntity += OnRemove;
    }

    void OnAdd(int ent)
    {
        Storage.GetComponent<LastRommTriggerCmp>(ent).onTriggetEnter += TriggerEnter;
    }

    void OnRemove(int ent)
    {
        Storage.GetComponent<LastRommTriggerCmp>(ent).onTriggetEnter -= TriggerEnter;
    }

    public void CustomUpdate()
    {
        
    }

    void TriggerEnter(int ent)
    {
        if (Storage.GetComponent<PlayerCmp>(ent) != null)
        {
            MidlleRoomDataCmp midlleRoomData = Storage.GetComponent<MidlleRoomDataCmp>(MidlleRoomDataGroup.GetEntitiesArray()[0]);


            //Storage.GetComponent<ButtonCmp>(midlleRoomData.last_active_button).meshRenderer.material = Storage.GetComponent<ButtonMaterialCmp>(midlleRoomData.last_active_button).Red;

            RigidbodyCmp Player = Storage.GetComponent<RigidbodyCmp>(ent);
            Player.transform.position += new Vector3(56, 0, 0);
        }
    }

    public void OnCustomDisable()
    {
        TriggerGroup.OnAddEntity -= OnAdd;
        TriggerGroup.OnBeforeRemoveEntity -= OnRemove;
    }
}
