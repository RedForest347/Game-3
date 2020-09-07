using RangerV;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTriggerCmp : ComponentBase
{
    public event Action<int> triggerEnter;
    public GameObject SpawnPoint;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EntityBase>() != null)
        {
            triggerEnter?.Invoke(other.GetComponent<EntityBase>().entity);
        }
    }
}
