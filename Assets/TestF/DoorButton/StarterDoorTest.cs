using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class StarterDoorTest : Starter
{
    public override void StarterSetup()
    {
        GlobalSystemStorage.Add<OpenCloseDoorProc>();
    }

   
}
