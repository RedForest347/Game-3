using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;
using TMPro;

public class PedestalProc : ProcessingBase, ICustomStart, ICustomUpdate, ICustomAwake
{
    Group PedestalGroup = Group.Create(new ComponentsList<PedestalCmp, ActivateCanvasCmp>(), new ComponentsList<OutDatedCmp>());
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp>());
    Group CameraGroup = Group.Create(new ComponentsList<CameraCmp>());
    Group CorridorDoorGroup = Group.Create(new ComponentsList<OutDatedCmp, CorridorDoorCmp>());

    int last_active_book;
    bool in_book;

    public void OnAwake()
    {
        in_book = false;
    }

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

        int book = Raycast();

        if (book != 0)
        {
            last_active_book = book;

            if (InZone(Storage.GetComponent<PedestalCmp>(book), EntityBase.GetEntity(PlayerGroup.GetEntitiesArray()[0]).transform.position))
            {
                GameObject UIElem = Storage.GetComponent<ActivateCanvasCmp>(book).UIElement;




                if (!UIElem.activeInHierarchy)
                {

                    GameObject Text = Storage.GetComponent<PedestalCmp>(book).Text;
                    if (Text != null && !Text.activeInHierarchy)
                        Text.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.F))
                    {

                        UIElem.SetActive(true);
                        SignalManager<ChangeMoveStateSignal>.Instance.SendSignal(new ChangeMoveStateSignal(false));
                        in_book = true;

                        Storage.GetComponent<PedestalCmp>(book).Text.SetActive(false);
                    }
                }
            }
        }
        else if (last_active_book != 0 && Storage.GetComponent<PedestalCmp>(last_active_book).Text.activeInHierarchy)
        {
            Storage.GetComponent<PedestalCmp>(last_active_book).Text.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Return) && in_book) // enter
        {
            GameObject UIElem = Storage.GetComponent<ActivateCanvasCmp>(PedestalGroup.GetEntitiesArray()[0]).UIElement;

            if (UIElem.activeInHierarchy)
            {
                SignalManager<ChangeMoveStateSignal>.Instance.SendSignal(new ChangeMoveStateSignal(true));

                SignalManager<CompleteWriteTextSignal>.Instance.SendSignal(new CompleteWriteTextSignal(UIElem.GetComponent<TMP_InputField>().text));

                UIElem.SetActive(false);

                EntityBase.GetEntity(last_active_book).AddCmp<OutDatedCmp>();
                //EntityBase.GetEntity(last_active_book).AddCmp<DoorLinkCmp>();
                //Debug.Log("is null " + (Storage.GetComponent<OutDatedCmp>(last_active_book) == null));
                //Debug.Log("Add");

                
                int corridor_door = CorridorDoorGroup.GetEntitiesArray()[0];
                //Storage.RemoveComponent<OutDatedCmp>(corridor_door);
                EntityBase.GetEntity(corridor_door).RemoveCmp<OutDatedCmp>();
                int button = Storage.GetComponent<ButtonLinkCmp>(corridor_door).Button.entity;
                Storage.GetComponent<ButtonCmp>(button).meshRenderer.material = Storage.GetComponent<ButtonMaterialCmp>(button).Green;
                in_book = false;

                Storage.GetComponent<PedestalCmp>(book).Text.SetActive(false);
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
