﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class LastRoomCompositionProc : ProcessingBase, ICustomAwake, ICustomStart, ICustomUpdate, ICustomDisable
{
    Group CompositionGroup = Group.Create(new ComponentsList<LastRoomCompositionCmp>());
    Group MidlleRoomDataGroup = Group.Create(new ComponentsList<MidlleRoomDataCmp>());
    Group ButtonGroup = Group.Create(new ComponentsList<ButtonAnimCmp, MidlleRoomButtonCmp>());
    Group LastTriggerGroup = Group.Create(new ComponentsList<LastRoomTriggerCmp>());
    Group MidlleCloneTriggerGroup = Group.Create(new ComponentsList<MidlleRoomCloneTriggerCmp>());


    public void OnAwake()
    {
        foreach(int button in ButtonGroup)
            AddEntityButton(button);

        ButtonGroup.OnAddEntity += AddEntityButton;
        ButtonGroup.OnBeforeRemoveEntity += AddEntityButton;


        foreach (int button in LastTriggerGroup)
            AddEntityLastTrigger(button);

        LastTriggerGroup.OnAddEntity += AddEntityLastTrigger;
        LastTriggerGroup.OnBeforeRemoveEntity += RemoveEntityLastTrigger;


        foreach (int button in MidlleCloneTriggerGroup)
            AddEntityMidlleClone(button);

        MidlleCloneTriggerGroup.OnAddEntity += AddEntityMidlleClone;
        MidlleCloneTriggerGroup.OnBeforeRemoveEntity += RemoveEntityMidlleClone;


       
    }

    public void OnStart()
    {
        DeactivateComposition();
    }

    #region Add/RemoveEnt

    public void AddEntityButton(int ent)
    {
        Storage.GetComponent<ButtonAnimCmp>(ent).OnPress += PressButton;
    }

    public void RemoveEntityButton(int ent)
    {
        Storage.GetComponent<ButtonAnimCmp>(ent).OnPress -= PressButton;
    }

    public void AddEntityLastTrigger(int ent)
    {
        Storage.GetComponent<LastRoomTriggerCmp>(ent).onTriggetEnter += LastTriggerEnter;
    }

    public void RemoveEntityLastTrigger(int ent)
    {
        Storage.GetComponent<LastRoomTriggerCmp>(ent).onTriggetEnter -= LastTriggerEnter;
    }

    public void AddEntityMidlleClone(int ent)
    {
        Storage.GetComponent<MidlleRoomCloneTriggerCmp>(ent).onTriggetEnter += MidlleCloneTriggerEnter;
    }

    public void RemoveEntityMidlleClone(int ent)
    {
        Storage.GetComponent<MidlleRoomCloneTriggerCmp>(ent).onTriggetEnter -= MidlleCloneTriggerEnter;
    }

    #endregion EndAdd/RemoveEnt


    void PressButton(int ent)
    {
        DeactivateComposition();
        ActivateComposition(ent);
    }

    void ActivateComposition(int ent)
    {
        int button_number = Storage.GetComponent<MidlleRoomButtonCmp>(ent).number;

        MidlleRoomDataCmp roomDataCmp = Storage.GetComponent<MidlleRoomDataCmp>(MidlleRoomDataGroup.GetEntitiesArray()[0]);
        int composition_level = roomDataCmp.current_composition_level;

        if (roomDataCmp.alreadyEnter.Contains(button_number))
            composition_level--;


        LastRoomCompositionCmp compositionCmp = Storage.GetComponent<LastRoomCompositionCmp>(CompositionGroup.GetEntitiesArray()[0]);


        if (composition_level >= 1)
            for (int i = 0; i < compositionCmp.FirstLevel.Count; i++)
                compositionCmp.FirstLevel[i].SetActive(true);

        if (composition_level >= 2)
            for (int i = 0; i < compositionCmp.SecondLevel.Count; i++)
                compositionCmp.SecondLevel[i].SetActive(true);

        if (composition_level >= 3)
            for (int i = 0; i < compositionCmp.ThirdLevel.Count; i++)
                compositionCmp.ThirdLevel[i].SetActive(true);

    }

    public void DeactivateComposition()
    {
        LastRoomCompositionCmp compositionCmp = Storage.GetComponent<LastRoomCompositionCmp>(CompositionGroup.GetEntitiesArray()[0]);

        for (int i = 0; i < compositionCmp.FirstLevel.Count; i++)
            compositionCmp.FirstLevel[i].SetActive(false);
        for (int i = 0; i < compositionCmp.SecondLevel.Count; i++)
            compositionCmp.SecondLevel[i].SetActive(false);
        for (int i = 0; i < compositionCmp.ThirdLevel.Count; i++)
            compositionCmp.ThirdLevel[i].SetActive(false);
        for (int i = 0; i < compositionCmp.JustDeactivate.Count; i++)
            compositionCmp.JustDeactivate[i].SetActive(false);
    }



    void LastTriggerEnter(int ent)
    {
        MidlleRoomDataCmp dataCmp = Storage.GetComponent<MidlleRoomDataCmp>(MidlleRoomDataGroup.GetEntitiesArray()[0]);

        int button = dataCmp.last_active_button;
        int number = Storage.GetComponent<MidlleRoomButtonCmp>(button).number;

        //Debug.Log("в итоге игрок вошел в комнату №" + number);

        if (dataCmp.alreadyEnter.Contains(number))
        {
            ReEnter();
        }
        else
        {
            dataCmp.alreadyEnter.Add(number);

            if (dataCmp.current_composition_level >= 3)
            {
                ActivateStairDoors(dataCmp);
                Debug.Log("Activate Doors");
            }

            dataCmp.current_composition_level++;
        }
        

    }

    void ActivateStairDoors(MidlleRoomDataCmp roomDataCmp)
    {
        roomDataCmp.StairDoor.SetActive(true);
        roomDataCmp.StairDoorClone.SetActive(true);
        roomDataCmp.StairDoorClone.transform.position = roomDataCmp.StairDoor.transform.position - new Vector3(56, 0, 0);
        //Debug.Log("StairDoor pos = " + roomDataCmp.StairDoor.transform.position + " clone pos = " + (roomDataCmp.StairDoor.transform.position + new Vector3(56, 0, 0)));
    }

    void ReEnter()// повторный вход в дверь, в которую уже входил
    {

    }

    void MidlleCloneTriggerEnter(int ent)
    {
        DeactivateComposition();
    }





    public void CustomUpdate()
    {
        
    }

    public void OnCustomDisable()
    {
        ButtonGroup.OnAddEntity -= AddEntityButton;
        ButtonGroup.OnBeforeRemoveEntity -= AddEntityButton;

        LastTriggerGroup.OnAddEntity -= AddEntityLastTrigger;
        LastTriggerGroup.OnBeforeRemoveEntity -= RemoveEntityLastTrigger;

        MidlleCloneTriggerGroup.OnAddEntity -= AddEntityMidlleClone;
        MidlleCloneTriggerGroup.OnBeforeRemoveEntity -= RemoveEntityMidlleClone;

    }
}
