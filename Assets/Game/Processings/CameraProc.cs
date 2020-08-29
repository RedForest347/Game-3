using RangerV;
using UnityEngine;

public class CameraProc : ProcessingBase, ICustomAwake, ICustomStart, ICustomUpdate, IReceive<ChangeMoveStateSignal>, IReceive<StartGameSignal>
{
    Group CameraGroup = Group.Create(new ComponentsList<CameraCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp>());

    bool need_camera;


    public void OnAwake()
    {
        need_camera = false;
    }


    public void OnStart()
    {
        SignalManager<ChangeMoveStateSignal>.Instance.AddReceiver(this);
        SignalManager<StartGameSignal>.Instance.AddReceiver(this);
    }

    public void CustomUpdate()
    {
        CameraRotate();
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
}
