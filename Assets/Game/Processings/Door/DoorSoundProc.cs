using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class DoorSoundProc : ProcessingBase, ICustomAwake
{
    Group DoorGroup = Group.Create(new ComponentsList<DoorAnimCmp, AudioCmp>(), new ComponentsList<OutDatedCmp>());

    public void OnAwake()
    {
        foreach (int door in DoorGroup)
        {
            OnAddEnt(door);
        }

        DoorGroup.OnAddEntity += OnAddEnt;
        DoorGroup.OnBeforeRemoveEntity += OnRemoveEnt;

    }

    void OnAddEnt(int ent)
    {
        Storage.GetComponent<DoorAnimCmp>(ent).OnStartOpen += StartOpenSound;
        Storage.GetComponent<DoorAnimCmp>(ent).OnStartClose += StartCloseSound;
    }

    void OnRemoveEnt(int ent)
    {
        if (Storage.GetComponent<DoorAnimCmp>(ent) == null)
            Debug.Log("is NULL ent = " + ent);

        Storage.GetComponent<DoorAnimCmp>(ent).OnStartOpen -= StartOpenSound;
        Storage.GetComponent<DoorAnimCmp>(ent).OnStartClose -= StartCloseSound;
    }


    void StartOpenSound(int door)
    {
        Debug.Log("StartOpenSound");
        AudioCmp audioCmp = Storage.GetComponent<AudioCmp>(door);

        audioCmp.audioSource.PlayOneShot(audioCmp.OpenSound);
    }


    void StartCloseSound(int door)
    {
        Debug.Log("StartCloseSound");
        AudioCmp audioCmp = Storage.GetComponent<AudioCmp>(door);

        audioCmp.audioSource.PlayOneShot(audioCmp.CloseSound);
    }


}
