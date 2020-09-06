using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;
using System;

public class EndStairTriggerCmp : ComponentBase
{
    public event Action<int> triggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EntityBase>() != null)
        {
            triggerEnter?.Invoke(other.GetComponent<EntityBase>().entity);
        }

    }
}
