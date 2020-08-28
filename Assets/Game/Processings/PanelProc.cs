using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class PanelProc : ProcessingBase, ICustomStart, IReceive<StartGameSignal>
{
    Group PanelGroup = Group.Create(new ComponentsList<PanelCmp>());

    public void OnStart()
    {
        SignalManager<StartGameSignal>.Instance.AddReceiver(this);
    }

    public void SignalHandler(StartGameSignal arg)
    {
        int panel = PanelGroup.GetEntitiesArray()[0];
        Storage.GetComponent<PanelCmp>(panel).anim.Play("ScreenDarkening");
        Storage.GetComponent<PanelCmp>(panel).OnEndAnim += EndAnim;
    }

    public void EndAnim()
    {
        Debug.Log("EndAnim");
        StartProc();
    }

    void StartProc()
    {
        //GlobalSystemStorage.Add<CameraProc>();
        //GlobalSystemStorage.Add<MoveProc>();
        //GlobalSystemStorage.Add<PressButtonProc>();
        //GlobalSystemStorage.Add<CloseDoorProc>();
        //GlobalSystemStorage.Add<DoorOCStateProc>();
        //GlobalSystemStorage.Add<PedestalProc>();
        //GlobalSystemStorage.Add<ShowTextProc>();
    }
}
