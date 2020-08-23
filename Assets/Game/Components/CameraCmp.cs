using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Component("Camera/CameraCmp")]
public class CameraCmp : ComponentBase, ICustomAwake
{
    new public Camera camera;
    public Vector3 offset_pos;
    public Vector3 offset_rotation;
    public Vector3 player_rotation;
    public float max_rotation_y;
    public float sensitivity;

    public void OnAwake()
    {
        camera = Camera.main;
    }
}
