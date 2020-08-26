using RangerV;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorProc : ProcessingBase, ICustomUpdate
{
    Group DoorGroup = Group.Create(new ComponentsList<DoorAnimCmp, DoorCloseAutomaticallyCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp>());

    public void CustomUpdate()
    {
        foreach (int door in DoorGroup)
        {
            DoorCloseAutomaticallyCmp doorComponent = EntityBase.GetEntity(door).GetComponent<DoorCloseAutomaticallyCmp>();
            DoorAnimCmp doorAnim = EntityBase.GetEntity(door).GetComponent<DoorAnimCmp>();

            foreach (int player in PlayerGroup)
            {
                Vector3 door_pos = doorComponent.transform.position;
                Vector3 player_pos = EntityBase.GetEntity(player).transform.position;

                if (!InZone(doorComponent, player_pos) && doorAnim.is_open)
                {
                    Debug.Log("дверь следует закрыть");
                    EntityBase.GetEntity(door).GetComponent<DoorAnimCmp>().anim.Play("DoorCloseAnim");
                }
            }
        }
    }

    bool InZone(DoorCloseAutomaticallyCmp door, Vector3 player_pos)
    {
        bool in_gorizontal;
        Vector2 door_pos = new Vector2(door.transform.position.x, door.transform.position.z);
        in_gorizontal = Vector2.Distance(door_pos, new Vector2(player_pos.x, player_pos.z)) <= door.switching_distances_gorizontal;
        return in_gorizontal;
    }
}
