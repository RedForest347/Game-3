using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMoveSignal : ISignal
{
    public bool signal_to_stop;

    public StopMoveSignal()
    {

    }

    public StopMoveSignal(bool signal_to_stop)
    {
        this.signal_to_stop = signal_to_stop;
    }
}
