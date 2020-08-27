﻿using RangerV;
using UnityEngine;

public class CameraProc : ProcessingBase, ICustomAwake, ICustomStart, ICustomUpdate, IReceive<StartStopMoveSignal>
{
    Group CameraGroup = Group.Create(new ComponentsList<CameraCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp>());

    bool need_camera;


    public void OnAwake()
    {
        need_camera = true;
    }


    public void OnStart()
    {
        SignalManager<StartStopMoveSignal>.Instance.AddReceiver(this);
    }

    public void CustomUpdate()
    {
        if (need_camera)
        {
            CameraRotate();
        }
    }

    void CameraRotate()
    {

        if (CameraGroup.entities_count != 1)
            Debug.LogError("в группе CameraGroup больше одной сущности");

        if (PlayerGroup.entities_count != 1)
            Debug.LogError("в группе PlayerGroup больше одной сущности");

        int player = PlayerGroup.GetEntitiesArray()[0];
        int camera = CameraGroup.GetEntitiesArray()[0];

        PlayerCmp playerCmp = Storage.GetComponent<PlayerCmp>(player);
        CameraCmp cameraCmp = Storage.GetComponent<CameraCmp>(camera);

        cameraCmp.player_rotation.x += -Input.GetAxis("Mouse Y") * cameraCmp.sensitivity;

        if (cameraCmp.player_rotation.x > cameraCmp.max_rotation_y)
            cameraCmp.player_rotation.x = cameraCmp.max_rotation_y;
        else if (cameraCmp.player_rotation.x < -cameraCmp.max_rotation_y)
            cameraCmp.player_rotation.x = -cameraCmp.max_rotation_y;



        cameraCmp.camera.transform.position = playerCmp.transform.position + playerCmp.transform.TransformVector(cameraCmp.offset_pos);
        cameraCmp.camera.transform.rotation = playerCmp.transform.rotation * Quaternion.Euler(cameraCmp.offset_rotation + cameraCmp.player_rotation);
    }

    public void SignalHandler(StartStopMoveSignal arg)
    {
        need_camera = arg.signal_to_start;
    }
}
