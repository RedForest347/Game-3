using RangerV;
using UnityEngine;

[Component("Door/ButtonCmp")]
public class ButtonCmp : ComponentBase
{
    public MeshRenderer meshRenderer;
    public KeyCode PressKey;
    public float activate_distance;
}
