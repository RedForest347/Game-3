using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class StarterDoorTest : Starter
{
    public override void StarterSetup()
    {
        //GlobalSystemStorage.Add<OpenCloseDoorProc>();
        GlobalSystemStorage.Add<CameraProc>();
        GlobalSystemStorage.Add<MoveProc>();
        GlobalSystemStorage.Add<CursorProc>();
        GlobalSystemStorage.Add<PressButtonProc>();
        GlobalSystemStorage.Add<CorutineManager>();
        GlobalSystemStorage.Add<CloseDoorProc>();
        GlobalSystemStorage.Add<DoorOCStateProc>();

    }

   
}
