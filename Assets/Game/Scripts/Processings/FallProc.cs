using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallProc : ProcessingBase, ICustomAwake, ICustomDisable
{
    Group FallTriggerGroup = Group.Create(new ComponentsList<FallTriggerCmp>());
    Group FallPanelCmp = Group.Create(new ComponentsList<SmoothTransitionCmp, FallPanelCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp>());

    public void OnAwake()
    {
        foreach (int trigger in FallTriggerGroup)
            AddTriggerEntity(trigger);

        FallTriggerGroup.OnAddEntity += AddTriggerEntity;
        FallTriggerGroup.OnBeforeRemoveEntity += RemoveTriggerEntity;



        foreach (int panel in FallPanelCmp)
            AddFallPanelEntity(panel);


        FallPanelCmp.OnAddEntity += AddFallPanelEntity;
        FallPanelCmp.OnBeforeRemoveEntity += RemoveFallPanelEntity;

    }


    void AddTriggerEntity(int ent)
    {
        Storage.GetComponent<FallTriggerCmp>(ent).triggerEnter += TriggerEnter;
    }


    void RemoveTriggerEntity(int ent)
    {
        Storage.GetComponent<FallTriggerCmp>(ent).triggerEnter -= TriggerEnter;
    }

    void AddFallPanelEntity(int ent)
    {
        Storage.GetComponent<SmoothTransitionCmp>(ent).screenDarkening += OnScreenDarkening;
        Storage.GetComponent<SmoothTransitionCmp>(ent).screenUndarkening += OnScreenUndarkening;
    }

    void RemoveFallPanelEntity(int ent)
    {
        Storage.GetComponent<SmoothTransitionCmp>(ent).screenDarkening -= OnScreenDarkening;
        Storage.GetComponent<SmoothTransitionCmp>(ent).screenUndarkening -= OnScreenUndarkening;
    }





    void TriggerEnter(int ent)
    {
        Debug.Log("Fall TriggerEnter");
        int panel = FallPanelCmp.GetEntitiesArray()[0];

        Storage.GetComponent<SmoothTransitionCmp>(panel).anim.Play();
    }


    void OnScreenDarkening()
    {
        SignalManager<StopMoveSignal>.Instance.SendSignal(new StopMoveSignal(true));

        int player = PlayerGroup.GetEntitiesArray()[0];
        int trigger = FallTriggerGroup.GetEntitiesArray()[0];
        Vector3 SpawnPos = Storage.GetComponent<FallTriggerCmp>(trigger).SpawnPoint.transform.position;

        Storage.GetComponent<FPSCmp>(player).controller.enabled = false;
        Storage.GetComponent<FPSCmp>(player).transform.position = SpawnPos;
        Storage.GetComponent<FPSCmp>(player).controller.enabled = true;


    }

    void OnScreenUndarkening()
    {
        SignalManager<StopMoveSignal>.Instance.SendSignal(new StopMoveSignal(false));
    }


    public void OnCustomDisable()
    {
        FallTriggerGroup.OnAddEntity -= AddTriggerEntity;
        FallTriggerGroup.OnBeforeRemoveEntity -= RemoveTriggerEntity;

        FallPanelCmp.OnAddEntity -= AddFallPanelEntity;
        FallPanelCmp.OnBeforeRemoveEntity -= RemoveFallPanelEntity;
    }
}
