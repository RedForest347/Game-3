using UnityEngine;
using RangerV;
using System.Xml.Schema;
using System;
using Stopwatch = System.Diagnostics.Stopwatch;

public class OpenCloseDoor : ProcessingBase, ICustomUpdate
{
    Group DoorGroup = Group.Create(new ComponentsList<Door>(), ComponentsList.Empty);
    Group PlayerGroup = Group.Create(new ComponentsList<Player>(), ComponentsList.Empty);
    int switching_distances_gorizontal = 10;
    int switching_distances_vertical = 3;

    public void CustomUpdate()
    {
        foreach (int door in DoorGroup)
        {
            Door doorComponent = EntityBase.GetEntity(door).GetComponent<Door>();

            foreach (int player in PlayerGroup)
            {
                Vector3 door_pos = doorComponent.transform.position;
                Vector3 player_pos = EntityBase.GetEntity(player).transform.position;

                if (InZone(doorComponent, player_pos))
                    Opening(doorComponent);
                else
                    Closing(doorComponent);
            }
        }
    }

    bool InZone(Door door, Vector3 player_pos)
    {
        bool in_gorizontal, in_vertical;
        Vector2 door_pos = new Vector2(door.transform.position.x, door.transform.position.z);

        in_gorizontal = Vector2.Distance(door_pos, new Vector2(player_pos.x, player_pos.z)) <= switching_distances_gorizontal;
        in_vertical = Math.Abs(door.start_pos_y - player_pos.y) <= switching_distances_vertical;

        return in_gorizontal && in_vertical;

    }

    void Opening(Door doorComponent)
    {
        //Debug.Log("Opening");
        if (doorComponent.pos > doorComponent.final_pos)
        {
            doorComponent.gameObject.transform.position -= new Vector3(0, doorComponent.step, 0);
            doorComponent.pos -= doorComponent.step;
        }
    }

    void Closing(Door doorComponent)
    {
        //Debug.Log("Closing");
        if (doorComponent.pos < 0)
        {
            doorComponent.gameObject.transform.position += new Vector3(0, doorComponent.step, 0);
            doorComponent.pos += doorComponent.step;
        }
    }
}
