using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class PanelProc : ProcessingBase
{
    Group PanelGroup = Group.Create(new ComponentsList<PanelCmp>());

    public void StartAnim()
    {
        int panel = PanelGroup.GetEntitiesArray()[0];
        Storage.GetComponent<PanelCmp>(panel).anim.Play("ScreenDarkening");
        Storage.GetComponent<PanelCmp>(panel).OnEndAnim += EndAnim;
    }

    public void EndAnim()
    {
        StartProc();
        SignalManager<StartGameSignal>.Instance.SendSignal(new StartGameSignal());
    }

    void StartProc()
    {
        //GlobalSystemStorage.Add<CameraProc>();
        //GlobalSystemStorage.Add<MoveProc>();

        GlobalSystemStorage.Add<FPSControllerProc>();
        GlobalSystemStorage.Add<PressButtonProc>();
        GlobalSystemStorage.Add<PressStairButtonProc>();
        GlobalSystemStorage.Add<CloseDoorProc>();
        GlobalSystemStorage.Add<DoorOCStateProc>();
        GlobalSystemStorage.Add<PedestalProc>();
        GlobalSystemStorage.Add<ShowTextProc>();
    }
}
