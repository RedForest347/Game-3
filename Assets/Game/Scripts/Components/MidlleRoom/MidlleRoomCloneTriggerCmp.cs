using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;
using System;

public class MidlleRoomCloneTriggerCmp : ComponentBase
{
    public event Action<int> onTriggetEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EntityBase>() != null)
            onTriggetEnter?.Invoke(other.gameObject.GetComponent<EntityBase>().entity);
    }

}
