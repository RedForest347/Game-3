using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorProc : ProcessingBase, ICustomUpdate, ICustomStart
{
    
    Group CursorGroup = Group.Create(new ComponentsList<CursorComp>());



    public void CustomUpdate()
    {
        
    }

    public void OnStart()
    {
        SetCursorPreset();
    }

    void DDD()
    {
        int cursor = CursorGroup.GetEntitiesArray()[0];


        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void SetCursorPreset()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
