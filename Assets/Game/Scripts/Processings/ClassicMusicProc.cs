using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class ClassicMusicProc : ProcessingBase, ICustomAwake, ICustomDisable
{
    Group ClassicMusicGroup = Group.Create(new ComponentsList<ClassicMusicDataCmp>());

    public void OnAwake()
    {
        foreach (int ent in ClassicMusicGroup)
        {
            Storage.GetComponent<ClassicMusicDataCmp>(ent);
        }

        ClassicMusicGroup.OnAddEntity += AddEntity;
        ClassicMusicGroup.OnBeforeRemoveEntity += RemoveEntity;
    }


    void AddEntity(int ent)
    {
        Storage.GetComponent<ClassicMusicDataCmp>(ent).triggerEnter += TriggerEnter;
    }

    void RemoveEntity(int ent)
    {
        Storage.GetComponent<ClassicMusicDataCmp>(ent).triggerEnter -= TriggerEnter;
    }


    void TriggerEnter(int ent)
    {
        StartMusic(ent);
    }

    void StartMusic(int ent)
    {
        //Debug.Log("StartMusic");
        int music = ClassicMusicGroup.GetEntitiesArray()[0];
        ClassicMusicDataCmp musicData = Storage.GetComponent<ClassicMusicDataCmp>(music);

        if (!musicData.music_started)
        {
            musicData.audioSource.PlayOneShot(musicData.audioClip);
            musicData.music_started = true;
        }

    }


    public void OnCustomDisable()
    {
        ClassicMusicGroup.OnAddEntity -= AddEntity;
        ClassicMusicGroup.OnBeforeRemoveEntity -= RemoveEntity;
    }
}
