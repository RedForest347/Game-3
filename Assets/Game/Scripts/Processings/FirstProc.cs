using RangerV;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Stopwatch = System.Diagnostics.Stopwatch;
using UnityEngine;

public class FirstProc : ProcessingBase, ICustomAwake, ICustomStart, ICustomUpdate
{
    Group FirstDataGroup = Group.Create(new ComponentsList<FirstDataHolderCmp>());

    [SerializeField]
    bool complete_work;


    public void OnAwake()
    {
        complete_work = false;
    }


    public void OnStart()
    {
        CorutineManager.StartCorutine(FirstSound());
    }

    public void CustomUpdate()
    {
        /*if (!complete_work)
        {
            int ent = FirstDataGroup.GetEntitiesArray()[0];
            FirstDataHolderCmp dataHolderCmp = Storage.GetComponent<FirstDataHolderCmp>(ent);
            dataHolderCmp.audioSource.PlayOneShot(dataHolderCmp.firstSteps, 1);
            complete_work = true;
            //dataHolderCmp.audioSource.

        }*/
    }

    IEnumerator FirstSound()
    {
        /*int ent = FirstDataGroup.GetEntitiesArray()[0];
        FirstDataHolderCmp dataHolderCmp = Storage.GetComponent<FirstDataHolderCmp>(ent);
        dataHolderCmp.audioSource.PlayOneShot(dataHolderCmp.firstSteps, 1);
        dataHolderCmp.audioSource.volume = 0;
        float length = dataHolderCmp.firstSteps.length;
        float volume_step = 1 / length;

        Stopwatch time = Stopwatch.StartNew();

        while (time.Elapsed.Seconds < length)
        {
            dataHolderCmp.audioSource.volume += volume_step * Time.deltaTime;
            yield return null;
        }
        dataHolderCmp.audioSource.PlayOneShot(dataHolderCmp.firstSteps, 1);

        time = Stopwatch.StartNew();

        while (time.Elapsed.Seconds < length)
            yield return null;*/

        yield return null;
        //Debug.Log("Send Signal");
        //Debug.Log();
        GlobalSystemStorage.Get<PanelProc>().StartAnim();
        //SignalManager<StartGameSignal>.Instance.SendSignal(new StartGameSignal());
    }
}
