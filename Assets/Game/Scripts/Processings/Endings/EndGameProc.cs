using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;
using Stopwatch = System.Diagnostics.Stopwatch;

public class EndGameProc : ProcessingBase, ICustomAwake, ICustomStart, ICustomDisable
{
    Group EndGameTriggerGroup = Group.Create(new ComponentsList<EndGameTriggerCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<FPSCmp, PlayerCmp>());
    Group FirstDataGroup = Group.Create(new ComponentsList<FirstDataHolderCmp>());
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
        CorutineManager.StartCorutine(FinalSound());

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

    IEnumerator FinalSound()
    {


        int ent = FirstDataGroup.GetEntitiesArray()[0];
        FirstDataHolderCmp dataHolderCmp = Storage.GetComponent<FirstDataHolderCmp>(ent);
        dataHolderCmp.audioSource.PlayOneShot(dataHolderCmp.firstSteps, 1);
        dataHolderCmp.audioSource.volume = 1;
        float length = dataHolderCmp.firstSteps.length;
        float volume_step = 1 / length / 2;

        Stopwatch time = Stopwatch.StartNew();

        while (time.Elapsed.Seconds < length)
        {
            dataHolderCmp.audioSource.volume -= volume_step * Time.deltaTime;
            yield return null;
        }
        dataHolderCmp.audioSource.PlayOneShot(dataHolderCmp.firstSteps, 0.4f);

        time = Stopwatch.StartNew();

        while (time.Elapsed.Seconds < length)
            yield return null;



    }



    public void OnCustomDisable()
    {
        EndGameTriggerGroup.OnAddEntity -= AddEntity;
        EndGameTriggerGroup.OnBeforeRemoveEntity -= RemoveEntity;
    }

}
