using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RangerV;
using System;

public class FPSControllerProc : ProcessingBase, ICustomAwake, ICustomFixedUpdate, IReceive<ChangeMoveStateSignal>, IReceive<StopMoveSignal>
{
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerCmp, FPSCmp>());

    bool need_move;

    public void OnAwake()
    {
        SignalManager<ChangeMoveStateSignal>.Instance.AddReceiver(this);
        SignalManager<StopMoveSignal>.Instance.AddReceiver(this);
        need_move = true;
    }

    public void CustomFixedUpdate()
    {
        if (need_move)
        {
            foreach (int player in PlayerGroup)
            {
                Move(player);
                Rotate(player);
                Gravity(player);
                Jump(player);
            }
        }
    }

    void Move(int entity)
    {
        FPSCmp fPSCmp = Storage.GetComponent<FPSCmp>(entity);
        MoveControls moveControls = fPSCmp.moveControls;

        float force = fPSCmp.move_force * Time.fixedDeltaTime * 100;
        Vector3 forceVector = Vector3.zero;

        if (Input.GetKey(moveControls.Forvard) || Input.GetKey(moveControls.Backward) && Input.GetKey(moveControls.Left) || Input.GetKey(moveControls.Rigt))
            force /= (float)Math.Sqrt(2);

        if (Input.GetKey(moveControls.Forvard))
            forceVector.x = force;
        else if (Input.GetKey(moveControls.Backward))
            forceVector.x = -force;

        if (Input.GetKey(moveControls.Left))
            forceVector.z = force;
        else if (Input.GetKey(moveControls.Rigt))
            forceVector.z = -force;

        forceVector /= 1000;


        fPSCmp.is_moving = forceVector.magnitude > 0.01f;



        fPSCmp.controller.Move(fPSCmp.transform.TransformDirection(forceVector));
    }

    void Rotate(int entity)
    {
        float rotation_force = Storage.GetComponent<FPSCmp>(entity).rotation_force;
        Vector3 MyAngle = new Vector3(0, Input.GetAxis("Mouse X") * rotation_force, 0);
        Storage.GetComponent<PlayerCmp>(entity).gameObject.transform.rotation *= Quaternion.Euler(MyAngle);
    }

    void Gravity(int entity)
    {
        FPSCmp fPSCmp = Storage.GetComponent<FPSCmp>(entity);
        CharacterController controller = fPSCmp.controller;


        fPSCmp.gravityData.current_vertical_velicity -= fPSCmp.gravityData.gravity * Time.fixedDeltaTime * 0.1f;

        if (Math.Abs(fPSCmp.gravityData.current_vertical_velicity) > 0.6f)
            fPSCmp.gravityData.current_vertical_velicity = Math.Sign(fPSCmp.gravityData.current_vertical_velicity) * 0.6f;

        if (fPSCmp.controller.isGrounded)
            fPSCmp.gravityData.current_vertical_velicity = 0;
        else
            controller.Move(new Vector3(0, fPSCmp.gravityData.current_vertical_velicity, 0));
    }


    void Jump(int entity)
    {
        FPSCmp fPSCmp = Storage.GetComponent<FPSCmp>(entity);
        CharacterController controller = fPSCmp.controller;

        if (controller.isGrounded && Input.GetKey(KeyCode.Space))
            fPSCmp.gravityData.current_vertical_velicity = fPSCmp.jump_force / 1000;
    }

    public void SignalHandler(ChangeMoveStateSignal arg)
    {
        need_move = arg.signal_to_start;
    }

    public void SignalHandler(StopMoveSignal arg)
    {
        need_move = !arg.signal_to_stop;

        if (!need_move)
        {
            foreach (int player in PlayerGroup)
            {
                Storage.GetComponent<FPSCmp>(player).is_moving = false;
            }

        }


    }
}
