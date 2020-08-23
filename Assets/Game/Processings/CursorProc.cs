using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorProc : ProcessingBase, ICustomUpdate
{
    Group CursorGroup = Group.Create(new ComponentsList<CursorComp>());

    public void CustomUpdate()
    {
        DDD();
    }

    void DDD()
    {
        int cursor = CursorGroup.GetEntitiesArray()[0];


        if (Input.GetKeyDown(KeyCode.P))
        {
            //Cursor.SetCursor(Storage.GetComponent<CursorComp>(cursor).cursorTexture, new Vector2(0, 0), CursorMode.Auto);
            Cursor.visible = !Cursor.visible;
        }
    }
}
