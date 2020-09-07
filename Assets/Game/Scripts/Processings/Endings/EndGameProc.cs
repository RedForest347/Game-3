using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class EndGameProc : ProcessingBase, ICustomAwake, ICustomStart, ICustomDisable
{
    Group EndGameTriggerGroup = Group.Create(new ComponentsList<EndGameTriggerCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<FPSCmp, PlayerCmp>());
    //Group TitleGroup = Group.Create(new ComponentsList<TitlesCmp>());

    public void OnAwake()
    {
        foreach (int trigger in EndGameTriggerGroup)
        {
            AddEntity(trigger);
        }

        EndGameTriggerGroup.OnAddEntity += AddEntity;
        EndGameTriggerGroup.OnBeforeRemoveEntity += RemoveEntity;

    }


    public void OnStart()
    {
        //TriggerEnter(-1);
    }

    void AddEntity(int ent)
    {
        Storage.GetComponent<EndGameTriggerCmp>(ent).triggerEnter += TriggerEnter;
    }

    void RemoveEntity(int ent)
    {
        Storage.GetComponent<EndGameTriggerCmp>(ent).triggerEnter -= TriggerEnter;
    }


    void TriggerEnter(int ent)
    {
        CorutineManager.StartCorutine(EndProcess());


        /*SignalManager<StopMoveSignal>.Instance.SendSignal(new StopMoveSignal(true));


        int player = PlayerGroup.GetEntitiesArray()[0];
        int end_trigger = EndGameTriggerGroup.GetEntitiesArray()[0];
        

        FPSCmp fpsCmp = Storage.GetComponent<FPSCmp>(player);
        EndGameTriggerCmp triggerCmp = Storage.GetComponent<EndGameTriggerCmp>(end_trigger);


        triggerCmp.GameNameAnim.Play();



        triggerCmp.endGamePanelAnim.Play();


        //triggerCmp.TitleHolder.SetActive(true);

        //int title = TitleGroup.GetEntitiesArray()[0];
        //TitlesCmp titlesCmp = Storage.GetComponent<TitlesCmp>(title);
        //titlesCmp.gameObject.SetActive(true);

        fpsCmp.controller.enabled = false;
        //fpsCmp.transform.position = triggerCmp.SpawnPoint.transform.position;
        //fpsCmp.transform.rotation = Quaternion.Euler(0, 180, 0);

        /*Debug.Log("Pos до = " + triggerCmp.TitleHolder.transform.position);
        Debug.Log("SpawnPoint = " + triggerCmp.SpawnPoint.transform.position);
        Debug.Log("TitleSpawnPoint = " + triggerCmp.TitleSpawnPoint.transform.position);
        triggerCmp.TitleHolder.transform.position = triggerCmp.TitleSpawnPoint.transform.position;
        Debug.Log("Pos после = " + triggerCmp.TitleHolder.transform.position);*/


        //titlesCmp.anim.Play();
        Debug.Log("Complete");
    }


    IEnumerator EndProcess()
    {
        SignalManager<StopMoveSignal>.Instance.SendSignal(new StopMoveSignal(true));

        int player = PlayerGroup.GetEntitiesArray()[0];
        int end_trigger = EndGameTriggerGroup.GetEntitiesArray()[0];

        FPSCmp fpsCmp = Storage.GetComponent<FPSCmp>(player);
        EndGameTriggerCmp triggerCmp = Storage.GetComponent<EndGameTriggerCmp>(end_trigger);

        fpsCmp.controller.enabled = false;


        triggerCmp.GameNameAnim.Play();

        while (triggerCmp.GameNameAnim.isPlaying)
            yield return null;

        triggerCmp.endGamePanelAnim.Play();

        while (triggerCmp.endGamePanelAnim.isPlaying)
            yield return null;

        Debug.Log("End");



        yield return null;
    }



    public void OnCustomDisable()
    {
        EndGameTriggerGroup.OnAddEntity -= AddEntity;
        EndGameTriggerGroup.OnBeforeRemoveEntity -= RemoveEntity;
    }

}
