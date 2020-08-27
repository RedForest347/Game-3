using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class PedestalSceneStarter : Starter
{
    public override void StarterSetup()
    {
        GlobalSystemStorage.Add<CameraProc>();
        GlobalSystemStorage.Add<MoveProc>();
        GlobalSystemStorage.Add<CursorProc>();
        GlobalSystemStorage.Add<PressButtonProc>();
        GlobalSystemStorage.Add<CorutineManager>();
        GlobalSystemStorage.Add<CloseDoorProc>();
        GlobalSystemStorage.Add<DoorOCStateProc>();
        GlobalSystemStorage.Add<PedestalProc>();

    }
}
