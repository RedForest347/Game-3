using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCmp : ComponentBase, ICustomAwake
{
    new public Camera camera;
    public Vector3 offset_pos;
    public Vector3 offset_rotation;

    public void OnAwake()
    {
        camera = Camera.main;
    }
}
