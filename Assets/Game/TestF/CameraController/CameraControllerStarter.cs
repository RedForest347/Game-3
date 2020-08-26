using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerStarter : Starter
{
    public override void StarterSetup()
    {
        GlobalSystemStorage.Add<MoveProc>();
        GlobalSystemStorage.Add<CameraProc>();
        GlobalSystemStorage.Add<CursorProc>();
    }
}
