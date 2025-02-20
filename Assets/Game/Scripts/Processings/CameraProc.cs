﻿using RangerV;
using UnityEngine;

public class CameraProc : ProcessingBase, ICustomAwake, ICustomStart, ICustomUpdate, IReceive<ChangeMoveStateSignal>, IReceive<StartGameSignal>, IReceive<StopMoveSignal>
{
    Group CameraGroup = Group.Create(new ComponentsList<CameraCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp>());

    public bool need_camera; // убрать public


    public void OnAwake()
    {
        need_camera = false;
    }


    public void OnStart()
    {
        SignalManager<ChangeMoveStateSignal>.Instance.AddReceiver(this);
        SignalManager<StartGameSignal>.Instance.AddReceiver(this);
        SignalManager<StopMoveSignal>.Instance.AddReceiver(this);
    }

    public void CustomUpdate()
    {
        CameraRotate();
    }

    void CameraRotate()
    {
        int player = PlayerGroup.GetEntitiesArray()[0];
        int camera = CameraGroup.GetEntitiesArray()[0];

        PlayerCmp playerCmp = Storage.GetComponent<PlayerCmp>(player);
        CameraCmp cameraCmp = Storage.GetComponent<CameraCmp>(camera);

        if (need_camera)
        {
            cameraCmp.player_rotation.x -= Input.GetAxis("Mouse Y") * cameraCmp.sensitivity;
        }

        if (cameraCmp.player_rotation.x > cameraCmp.max_rotation_y)
            cameraCmp.player_rotation.x = cameraCmp.max_rotation_y;
        else if (cameraCmp.player_rotation.x < -cameraCmp.max_rotation_y)
            cameraCmp.player_rotation.x = -cameraCmp.max_rotation_y;

        cameraCmp.camera.transform.position = playerCmp.transform.position + playerCmp.transform.TransformVector(cameraCmp.offset_pos);
        cameraCmp.camera.transform.rotation = playerCmp.transform.rotation * Quaternion.Euler(cameraCmp.offset_rotation + cameraCmp.player_rotation);
    }

    public void SignalHandler(ChangeMoveStateSignal arg)
    {
        need_camera = arg.signal_to_start;
    }

    public void SignalHandler(StartGameSignal arg)
    {
        need_camera = true;
    }

    public void SignalHandler(StopMoveSignal arg)
    {
        need_camera = !arg.signal_to_stop;
    }
}
