using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;
using System;

public class FPSCmp : ComponentBase, ICustomAwake
{
    public bool is_moving;

    public float move_force;
    public float jump_force;
    public float rotation_force;

    public GravityData gravityData;
    public MoveControls moveControls;

    [HideInInspector]
    public CharacterController controller;

    public void OnAwake()
    {
        is_moving = false;
        controller = GetComponent<CharacterController>();
    }
}

[System.Serializable]
public class GravityData
{
    [HideInInspector]
    public float current_vertical_velicity;
    public float gravity;

}