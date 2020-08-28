using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStarter : Starter
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
