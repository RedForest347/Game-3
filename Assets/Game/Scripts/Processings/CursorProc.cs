using RangerV;
using UnityEngine;

public class CursorProc : ProcessingBase, ICustomAwake, ICustomStart, IReceive<ChangeMoveStateSignal>
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
        SignalManager<ChangeMoveStateSignal>.Instance.AddReceiver(this);
    }

    public void SignalHandler(ChangeMoveStateSignal arg)
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
