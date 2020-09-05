using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class MidlleRoomDataCmp : ComponentBase, ICustomAwake
{
    public EntityBase[] MidlleRoomDoors;
    public int last_active_button;
    public int current_composition_level;

    public GameObject StairDoor;
    public GameObject StairDoorClone;

    public List<int> alreadyEnter;

    public void OnAwake()
    {
        current_composition_level = 1;
        alreadyEnter = new List<int>();
    }
}
