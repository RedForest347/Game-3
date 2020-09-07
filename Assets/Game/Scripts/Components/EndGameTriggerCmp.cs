using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;
using System;

public class EndGameTriggerCmp : ComponentBase
{
    public bool is_good_end;
    public event Action<int> triggerEnter;

    //public GameObject SpawnPoint;
    //public GameObject TitleSpawnPoint;
    //public GameObject TitleHolder;
    //public GameObject Level;
    public Animation endGamePanelAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EntityBase>() != null)
            triggerEnter?.Invoke(other.GetComponent<EntityBase>().entity);
    }
}
