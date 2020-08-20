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

        int sensitivity = 1;
        float new_x_pos = cameraCmp.camera.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * sensitivity;

        cameraCmp.camera.transform.position = playerCmp.transform.position + playerCmp.transform.TransformVector(cameraCmp.offset_pos);
        cameraCmp.camera.transform.rotation = playerCmp.transform.rotation * Quaternion.Euler(cameraCmp.camera.transform.rotation.eulerAngles + new Vector3(new_x_pos, 0, 0));
        //cameraCmp.camera.transform.rotation *= Quaternion.Euler(Input.GetAxis("Mouse Y") + cameraCmp.camera.transform.rotation.x, 0, 0);

        

        //cameraCmp.camera.transform.rotation = Quaternion.Euler(new_x_pos, 0, 0);

        //MyAngle = new Vector3(0, Input.GetAxis("Mouse X") * sensitivity.y, Input.GetAxis("Mouse Y") * sensitivity.z);
    }
}
