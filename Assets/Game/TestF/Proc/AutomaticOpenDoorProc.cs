using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;
using System;

public class AutomaticOpenDoorProc : ProcessingBase, ICustomUpdate
{
    Group AutomaticOpenDoorGroup = Group.Create(new ComponentsList<AutomaticOpenDoorCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp>());



    public void CustomUpdate()
    {
        int player = PlayerGroup.GetEntitiesArray()[0];

        foreach (int door in AutomaticOpenDoorGroup)
        {
            AutomaticOpenDoorCmp doorCmp = Storage.GetComponent<AutomaticOpenDoorCmp>(door);

            if (InZone(doorCmp, EntityBase.GetEntity(player)))
            {

                float distance = Distance(doorCmp, EntityBase.GetEntity(player));

                //y = kx +b
                //k = (y1 - y2) / (x1 - x2)
                //b = y2 - k * x2
                //x = distance

                float x1 = doorCmp.full_open_distance;
                float x2 = doorCmp.switching_distances_gorizontal;
                float y1 = 90;
                float y2 = 0;

                float k = (y1 - y2) / (x1 - x2);
                float b = y2 - k * x2;

                float angle = Mathf.Clamp(k * distance + b, 0, 90);

                doorCmp.transform.rotation = Quaternion.Euler(doorCmp.transform.rotation.x, angle, doorCmp.transform.rotation.z);
                
            }
        }

    }


    bool InZone(AutomaticOpenDoorCmp doorCmp, EntityBase Player)
    {
        bool in_gorizontal;
        Vector2 door_pos = new Vector2(doorCmp.transform.position.x, doorCmp.transform.position.z);
        Vector3 player_pos = Player.transform.position;


        in_gorizontal = Vector2.Distance(door_pos, new Vector2(player_pos.x, player_pos.z)) <= doorCmp.switching_distances_gorizontal;
        return in_gorizontal;
    }

    float Distance(AutomaticOpenDoorCmp Door, EntityBase Player)
    {
        float in_gorizontal;
        Vector2 door_pos = new Vector2(Door.transform.position.x, Door.transform.position.z);
        Vector3 player_pos = Player.transform.position;


        in_gorizontal = Vector2.Distance(door_pos, new Vector2(player_pos.x, player_pos.z));
        return in_gorizontal;
    }

}
