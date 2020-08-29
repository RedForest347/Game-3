using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTestStarter : Starter
{

    public override void StarterSetup()
    {
        GlobalSystemStorage.Add<CameraProc>();
        GlobalSystemStorage.Add<MoveProc>();
        //GlobalSystemStorage.Add<OpenCloseDoorProc>();
    }

    
}
