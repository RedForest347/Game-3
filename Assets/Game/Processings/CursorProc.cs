using RangerV;
using UnityEngine;

public class CursorProc : ProcessingBase, ICustomAwake, ICustomStart, IReceive<StartStopMoveSignal>
{
    //Group CursorGroup = Group.Create(new ComponentsList<CursorComp>());
    CursorLockMode previous_state;

    public void OnAwake()
    {
        previous_state = CursorLockMode.None;
    }

    public void OnStart()
    {
        SetCursorPreset(CursorLockMode.Locked);
        SignalManager<StartStopMoveSignal>.Instance.AddReceiver(this);
    }

    public void SignalHandler(StartStopMoveSignal arg)
    {
        if (arg.signal_to_start)
            SetCursorPreset(CursorLockMode.Locked);
        else
            SetCursorPreset(previous_state);
    }

    void SetCursorPreset(CursorLockMode mode)
    {
        Cursor.lockState = mode;
    }
}
