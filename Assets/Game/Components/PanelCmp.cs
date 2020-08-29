using RangerV;
using System;
using UnityEngine;

public class PanelCmp : ComponentBase, ICustomAwake
{
    public event Action OnEndAnim;
    public Animation anim;

    public void OnAwake()
    {
        anim = GetComponent<Animation>();
    }


    public void EndAnim()
    {
        OnEndAnim?.Invoke();
    }
}
