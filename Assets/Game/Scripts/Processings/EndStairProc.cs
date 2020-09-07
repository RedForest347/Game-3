using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class EndStairProc : ProcessingBase, ICustomAwake, ICustomUpdate, ICustomDisable
{
    Group EndStairTriggerGroup = Group.Create(new ComponentsList<EndStairTriggerCmp>());
    Group EndCorridorGroup = Group.Create(new ComponentsList<EndCorridorDataCmp>());
    Group EndCorridorPanelGroup = Group.Create(new ComponentsList<SmoothTransitionCmp, EndCorridorPanelCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp, FPSCmp>());


    public void OnAwake()
    {
        foreach (int end_stair in EndStairTriggerGroup)
            TriggerAddEntity(end_stair);

        EndStairTriggerGroup.OnAddEntity += TriggerAddEntity;
        EndStairTriggerGroup.OnBeforeRemoveEntity += TriggerRemoveEntity;


        foreach (int end_panel in EndCorridorPanelGroup)
            EndPanelAddEntity(end_panel);

        EndCorridorPanelGroup.OnAddEntity += EndPanelAddEntity;
        EndCorridorPanelGroup.OnBeforeRemoveEntity += EndPanelRemoveEntity;

    }

    void TriggerAddEntity(int ent)
    {
        Storage.GetComponent<EndStairTriggerCmp>(ent).triggerEnter += TriggerEnter;
    }

    void TriggerRemoveEntity(int ent)
    {
        Storage.GetComponent<EndStairTriggerCmp>(ent).triggerEnter -= TriggerEnter;
    }

    void EndPanelAddEntity(int ent)
    {
        Storage.GetComponent<SmoothTransitionCmp>(ent).screenDarkening += ScreenDarkening;
        Storage.GetComponent<SmoothTransitionCmp>(ent).screenUndarkening += ScreenUndarkening;
    }

    void EndPanelRemoveEntity(int ent)
    {
        Storage.GetComponent<SmoothTransitionCmp>(ent).screenDarkening -= ScreenDarkening;
        Storage.GetComponent<SmoothTransitionCmp>(ent).screenUndarkening -= ScreenUndarkening;
    }

    void TriggerEnter(int player)
    {
        //Debug.Log(EntityBase.GetEntity(player).gameObject.name + " вошел в триггер");
        int end_panel = EndCorridorPanelGroup.GetEntitiesArray()[0];
        Storage.GetComponent<SmoothTransitionCmp>(end_panel).gameObject.SetActive(true);
        Storage.GetComponent<SmoothTransitionCmp>(end_panel).anim.Play();

        SignalManager<StopMoveSignal>.Instance.SendSignal(new StopMoveSignal(true));


    }

    void ScreenDarkening()
    {
        //Debug.Log("ScreenDarkening");
        EndCorridorDataCmp endData = Storage.GetComponent<EndCorridorDataCmp>(EndCorridorGroup.GetEntitiesArray()[0]);

        for (int i = 0; i < endData.meshRenderers.Count; i++)
        {
            endData.meshRenderers[i].enabled = true;
        }

        //endData.EndCorridor.SetActive(true);

        int player = PlayerGroup.GetEntitiesArray()[0];

        FPSCmp fpsCmp = Storage.GetComponent<FPSCmp>(player);

        fpsCmp.controller.enabled = false;
        fpsCmp.transform.position = endData.SpawnPoint.transform.position;
        fpsCmp.controller.enabled = true;




    }

    void ScreenUndarkening()
    {
        //Debug.Log("ScreenUndarkening");
        SignalManager<StopMoveSignal>.Instance.SendSignal(new StopMoveSignal(false));

        int end_panel = EndCorridorPanelGroup.GetEntitiesArray()[0];
        Storage.GetComponent<EndCorridorPanelCmp>(end_panel).gameObject.SetActive(false);
    }


    IEnumerator StartFinalCorridor(int ent)
    {
       

        yield return null;
    }

    public void CustomUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public void OnCustomDisable()
    {
        EndStairTriggerGroup.OnAddEntity -= TriggerAddEntity;
        EndStairTriggerGroup.OnBeforeRemoveEntity -= TriggerRemoveEntity;

        EndCorridorPanelGroup.OnAddEntity -= EndPanelAddEntity;
        EndCorridorPanelGroup.OnBeforeRemoveEntity -= EndPanelRemoveEntity;
    }
}
