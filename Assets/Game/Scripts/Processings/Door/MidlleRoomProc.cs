using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class MidlleRoomProc : ProcessingBase, ICustomUpdate, ICustomAwake, ICustomDisable
{
    //Group MidlleRoomDoorsGroup = Group.Create(new ComponentsList<>());
    Group LastRTriggerGroup = Group.Create(new ComponentsList<LastRoomTriggerCmp>());
    Group MidlleCloneTriggerGroup = Group.Create(new ComponentsList<MidlleRoomCloneTriggerCmp>());
    Group MidlleRoomDataGroup = Group.Create(new ComponentsList<MidlleRoomDataCmp>());


    public void OnAwake()
    {
        foreach (int data in LastRTriggerGroup)
            Storage.GetComponent<LastRoomTriggerCmp>(data).onTriggetEnter += LastRoomTriggerEnter;

        LastRTriggerGroup.OnAddEntity += OnAddLast;
        LastRTriggerGroup.OnBeforeRemoveEntity += OnRemoveLast;




        foreach (int data in MidlleCloneTriggerGroup)
        {
            Debug.Log("ent = " + data + " Storage.GetComponent<MidlleRoomCloneTriggerCmp>(data) = " + 
                Storage.GetComponent<MidlleRoomCloneTriggerCmp>(data));
            Storage.GetComponent<MidlleRoomCloneTriggerCmp>(data).onTriggetEnter += MidlleCloneTriggerEnter;
        }

        MidlleCloneTriggerGroup.OnAddEntity += OnAddMidlleClone;
        MidlleCloneTriggerGroup.OnBeforeRemoveEntity += OnRemoveMidlleClone;
    }

    void OnAddLast(int ent)
    {
        Storage.GetComponent<LastRoomTriggerCmp>(ent).onTriggetEnter += LastRoomTriggerEnter;
    }

    void OnRemoveLast(int ent)
    {
        Storage.GetComponent<LastRoomTriggerCmp>(ent).onTriggetEnter -= LastRoomTriggerEnter;
    }



    void OnAddMidlleClone(int ent)
    {
        Storage.GetComponent<MidlleRoomCloneTriggerCmp>(ent).onTriggetEnter += MidlleCloneTriggerEnter;
    }

    void OnRemoveMidlleClone(int ent)
    {
        Storage.GetComponent<MidlleRoomCloneTriggerCmp>(ent).onTriggetEnter -= MidlleCloneTriggerEnter;
    }

    public void CustomUpdate()
    {
        
    }

    void LastRoomTriggerEnter(int ent)
    {
        if (Storage.GetComponent<PlayerCmp>(ent) != null)
        {
            //Debug.Log("Enter LastRoomTriggerEnter");

            MidlleRoomDataCmp midlleRoomData = Storage.GetComponent<MidlleRoomDataCmp>(MidlleRoomDataGroup.GetEntitiesArray()[0]);

            if (midlleRoomData.last_active_button != 0)
            {
                Material Red = Storage.GetComponent<ButtonMaterialCmp>(midlleRoomData.last_active_button).Red;

                Storage.GetComponent<ButtonCmp>(midlleRoomData.last_active_button).meshRenderer.material = Red;
                Storage.GetComponent<MidlleRoomButtonCmp>(midlleRoomData.last_active_button).CloneButton.material = Red;
            }
        }
    }

    void MidlleCloneTriggerEnter(int ent)
    {
        if (Storage.GetComponent<PlayerCmp>(ent) != null)
        {
            //Debug.Log("Enter MidlleCloneTriggerEnter");

            RigidbodyCmp Player = Storage.GetComponent<RigidbodyCmp>(ent);
            Player.transform.position += new Vector3(56, 0, 0);
        }
    }


    public void OnCustomDisable()
    {
        LastRTriggerGroup.OnAddEntity -= OnAddLast;
        LastRTriggerGroup.OnBeforeRemoveEntity -= OnRemoveLast;
    }
}
