using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;
using System;

public class PressButtonProc : ProcessingBase, ICustomUpdate, ICustomAwake, ICustomDisable
{
    Group CameraGroup = Group.Create(new ComponentsList<CameraCmp>());
    Group ButtonGroup = Group.Create(new ComponentsList<ButtonCmp, ButtonAnimCmp>(), new ComponentsList<MidlleRoomButtonCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp>());


    public void OnAwake()
    {
        foreach (int button in ButtonGroup)
            OnAddEnt(button);

        ButtonGroup.OnAddEntity += OnAddEnt;
        ButtonGroup.OnBeforeRemoveEntity += OnRemoveEnt;
    }

    public void OnAddEnt(int ent)
    {
        Storage.GetComponent<ButtonAnimCmp>(ent).OnPress += StartOpenDoor;
    }

    public void OnRemoveEnt(int ent)
    {
        if (Storage.GetComponent<ButtonAnimCmp>(ent) == null)
            Debug.Log("is NULL");
        Storage.GetComponent<ButtonAnimCmp>(ent).OnPress -= StartOpenDoor;
    }

    public void CustomUpdate()
    {
        PressButton();
    }

    void StartOpenDoor(int button_sender)
    {
        int door = Storage.GetComponent<ButtonAnimCmp>(button_sender).doorHolder.GetComponent<EntityBase>().entity;

        if (Storage.GetComponent<OutDatedCmp>(door) == null)
        {
            DoorAnimCmp doorAnimCmp = Storage.GetComponent<DoorAnimCmp>(door);
            doorAnimCmp.anim.Play("DoorOpenAnim");
        }
    }

    void PressButton()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            int button = ButtonRay();
             
            if (button != 0 && ButtonGroup.Contains(button))
            {
                if (InZone(Storage.GetComponent<ButtonCmp>(button), EntityBase.GetEntity(PlayerGroup.GetEntitiesArray()[0]).transform.position))
                {
                    Animation animation = Storage.GetComponent<ButtonAnimCmp>(button).anim;
                    bool door_is_closed = Storage.GetComponent<ButtonAnimCmp>(button).doorHolder.GetComponent<DoorAnimCmp>().is_close;
                    if (!animation.isPlaying && door_is_closed)
                    {
                        animation.Play();
                    }
                }
            }
        }
    }

    int ButtonRay()
    {
        int camera = CameraGroup.GetEntitiesArray()[0];
        Transform PlayerTransform = EntityBase.GetEntity(camera).gameObject.transform;

        Ray ray = new Ray(PlayerTransform.position, PlayerTransform.forward);
        Debug.DrawLine(PlayerTransform.position, PlayerTransform.position + PlayerTransform.forward * 10, Color.blue);

        Physics.Raycast(ray, out RaycastHit raycastHit, 10);

        int door = raycastHit.collider?.GetComponent<EntityBase>()?.entity ?? 0;

        if (door != 0 && ButtonGroup.Contains(door))
        {
            //Debug.Log("ButtonGroup.entities_count = " + ButtonGroup.entities_count + " ButtonGroup.Contains(" + door + ")" + " door = " + (Storage.GetComponent<ButtonComp>(door) == null ? "NULL" : "NOT NULL"));
            return door;
        }
        return 0;
    }

    bool InZone(ButtonCmp door, Vector3 player_pos)
    {
        bool in_gorizontal;
        Vector2 door_pos = new Vector2(door.transform.position.x, door.transform.position.z);
        in_gorizontal = Vector2.Distance(door_pos, new Vector2(player_pos.x, player_pos.z)) <= door.activate_distance;
        return in_gorizontal;
    }

    public void OnCustomDisable()
    {
        ButtonGroup.OnAddEntity -= OnAddEnt;
        ButtonGroup.OnAfterRemoveEntity -= OnRemoveEnt;
    }
}
