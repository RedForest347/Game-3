using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;

public class PedestalProc : ProcessingBase, ICustomStart, ICustomUpdate
{
    Group PedestalGroup = Group.Create(new ComponentsList<PedestalCmp, ActivaleCanvasCmp>());
    //Group BookGroup = Group.Create(new ComponentsList<PedestalCmp, ActivaleCanvasCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp>());
    Group CameraGroup = Group.Create(new ComponentsList<CameraCmp>());

    public void OnStart()
    {
        //SignalManager<StopMoveSignal>.Instance.AddReceiver(this);
        //SignalManager<StopMoveSignal>.Instance.SendSignal(new StopMoveSignal());
    }

    public void CustomUpdate()
    {
        PressButton();
    }

    void PressButton()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            int book = Raycast();

            if (book != 0)
            {
                if (InZone(Storage.GetComponent<PedestalCmp>(book), EntityBase.GetEntity(PlayerGroup.GetEntitiesArray()[0]).transform.position))
                {
                    GameObject UIElem = Storage.GetComponent<ActivaleCanvasCmp>(book).UIElement;
                    if (!UIElem.activeInHierarchy)
                    {
                        UIElem.SetActive(true);
                        SignalManager<StartStopMoveSignal>.Instance.SendSignal(new StartStopMoveSignal(false));
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return)) // enter
        {
            GameObject UIElem = Storage.GetComponent<ActivaleCanvasCmp>(PedestalGroup.GetEntitiesArray()[0]).UIElement;
            if (UIElem.activeInHierarchy)
            {
                SignalManager<StartStopMoveSignal>.Instance.SendSignal(new StartStopMoveSignal(true));
                UIElem.SetActive(false);
            }
        }
    }

    int Raycast()
    {
        int camera = CameraGroup.GetEntitiesArray()[0];
        Transform PlayerTransform = EntityBase.GetEntity(camera).gameObject.transform;

        Ray ray = new Ray(PlayerTransform.position, PlayerTransform.forward);
        Debug.DrawLine(PlayerTransform.position, PlayerTransform.position + PlayerTransform.forward * 10, Color.blue);

        Physics.Raycast(ray, out RaycastHit raycastHit, 10);

        int door = raycastHit.collider?.GetComponent<EntityBase>()?.entity ?? 0;

        if (door != 0 && PedestalGroup.Contains(door))
        {
            return door;
        }
        return 0;
    }

    bool InZone(PedestalCmp pedestal, Vector3 player_pos)
    {
        bool in_gorizontal;
        Vector2 door_pos = new Vector2(pedestal.transform.position.x, pedestal.transform.position.z);
        in_gorizontal = Vector2.Distance(door_pos, new Vector2(player_pos.x, player_pos.z)) <= pedestal.activate_distance;
        return in_gorizontal;
    }

    /*public void SignalHandler(StopMoveSignal arg)
    {
        Debug.Log("сигнал получен");
    }*/
}
