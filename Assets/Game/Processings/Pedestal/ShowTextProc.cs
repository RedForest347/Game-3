using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextProc : ProcessingBase, ICustomAwake, IReceive<CompleteWriteTextSignal>
{
    Group TextGroup = Group.Create(new ComponentsList<TextCmp>());

    public void OnAwake()
    {
        //Debug.Log("Add");
        SignalManager<CompleteWriteTextSignal>.Instance.AddReceiver(this);
    }

    public void SignalHandler(CompleteWriteTextSignal arg)
    {
        int text = TextGroup.GetEntitiesArray()[0];
        Storage.GetComponent<TextCmp>(text).textMesh.text = arg.text;

    }
}
