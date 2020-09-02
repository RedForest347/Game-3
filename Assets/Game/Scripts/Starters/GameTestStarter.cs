using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTestStarter : Starter
{

    public override void StarterSetup()
    {
        //GlobalSystemStorage.Add<CursorProc>();
        GlobalSystemStorage.Add<CorutineManager>();
        //GlobalSystemStorage.Add<FirstProc>();
        //GlobalSystemStorage.Add<PanelProc>();
        GlobalSystemStorage.Add<MoveProc>();
        //GlobalSystemStorage.Add<PressButtonProc>();
        //GlobalSystemStorage.Add<CloseDoorProc>();
        //GlobalSystemStorage.Add<DoorOCStateProc>();
        //GlobalSystemStorage.Add<PedestalProc>();
        //GlobalSystemStorage.Add<ShowTextProc>();
        GlobalSystemStorage.Add<CameraProc>();
        //GlobalSystemStorage.Add<StepProc>();
        //GlobalSystemStorage.Add<DoorSoundProc>();
        //GlobalSystemStorage.Add<PressMidlleRoomButtonProc>();
        //GlobalSystemStorage.Add<MidlleRoomProc>();
    }

    
}
