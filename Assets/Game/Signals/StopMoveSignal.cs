using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStopMoveSignal : ISignal
{
    //public object sender;
    //public int important_level;
    public bool signal_to_start;

    public StartStopMoveSignal()
    {
        this.signal_to_start = false;
    }

    public StartStopMoveSignal(bool signal_to_start)
    {
        this.signal_to_start = signal_to_start;
    }
}
