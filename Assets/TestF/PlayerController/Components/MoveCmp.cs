﻿using RangerV;
//using UnityEngine;

[Component("PlayerController/MoveCmp")]
public class MoveCmp : ComponentBase
{
    public float move_speed;
    public float max_speed;
    public float rotate_speed;
    public float max_rotate_speed;

    public MoveControls moveControls;
     
}
