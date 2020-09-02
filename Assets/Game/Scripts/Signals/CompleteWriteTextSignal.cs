using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class CompleteWriteTextSignal : ISignal
{
    public string text;

    public CompleteWriteTextSignal()
    {

    }

    public CompleteWriteTextSignal(string text)
    {
        this.text = text;
    }
}
