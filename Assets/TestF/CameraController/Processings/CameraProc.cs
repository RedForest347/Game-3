using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProc : ProcessingBase, ICustomUpdate
{
    Group CameraGroup = Group.Create(new ComponentsList<CameraCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp>());

    public void CustomUpdate()
    {
        if (CameraGroup.entities_count != 1)
            Debug.LogError("в группе CameraGroup больше одной сущности");

        if (PlayerGroup.entities_count != 1)
            Debug.LogError("в группе PlayerGroup больше одной сущности");

        int player = PlayerGroup.GetEntitiesArray()[0];
        int camera = CameraGroup.GetEntitiesArray()[0];

        PlayerCmp playerCmp = Storage.GetComponent<PlayerCmp>(player);
        CameraCmp cameraCmp = Storage.GetComponent<CameraCmp>(camera);


        cameraCmp.camera.transform.position = playerCmp.transform.position + playerCmp.transform.InverseTransformVector(cameraCmp.offset_pos);
        cameraCmp.camera.transform.rotation = playerCmp.transform.rotation * Quaternion.Euler(cameraCmp.offset_rotation);
    }
}
