using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class TextCmp : ComponentBase, ICustomAwake
{
    public TextMesh textMesh;

    public void OnAwake()
    {
        textMesh = GetComponent<TextMesh>();
    }

}
