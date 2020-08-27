using RangerV;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProc : ProcessingBase, ICustomFixedUpdate, ICustomUpdate, ICustomStart, ICustomAwake, IReceive<StartStopMoveSignal>
{
    Group MoveGroup = Group.Create(new ComponentsList<PlayerCmp, MoveCmp, RigidbodyCmp>());
    Group JumpGroup = Group.Create(new ComponentsList<JumpCmp, RigidbodyCmp>());

    bool need_move;
    bool need_rotate;


    public void OnAwake()
    {
        need_move = true;
        need_rotate = true;
    }

    public void OnStart()
    {
        SignalManager<StartStopMoveSignal>.Instance.AddReceiver(this);
    }


    public void CustomUpdate()
    {
        if (need_rotate)
        {
            foreach (int player in MoveGroup)
            {
                //Move(player);
                Rotate(player);
            }
        }
    }

    public void CustomFixedUpdate()
    {
        if (need_move)
        {
            //Debug.Log("DDD");
            foreach (int player in MoveGroup)
            {
                Move(player);
                //Rotate(player);
            }

            foreach (int player in JumpGroup)
            {
                Jump(player);
            }
        }
    }


    void Move(int entity)
    {
        MoveCmp moveCmp = Storage.GetComponent<MoveCmp>(entity);
        Rigidbody rigidbody = Storage.GetComponent<RigidbodyCmp>(entity).rigidbody;
        MoveControls moveControls = moveCmp.moveControls;

        float force = moveCmp.move_speed * Time.fixedDeltaTime;

        if (Input.GetKey(moveControls.Forvard))
            rigidbody.AddRelativeForce(new Vector3(force, 0, 0));
        else if (Input.GetKey(moveControls.Backward))
            rigidbody.AddRelativeForce(new Vector3(-force, 0, 0));

        if (Input.GetKey(moveControls.Left))
            rigidbody.AddRelativeForce(new Vector3(0, 0, force));
        else if (Input.GetKey(moveControls.Rigt))
            rigidbody.AddRelativeForce(new Vector3(0, 0, -force));


        Vector2 gorizontal_speed = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);

        if (gorizontal_speed.magnitude > moveCmp.max_move_speed)
        {
            gorizontal_speed = gorizontal_speed.normalized * moveCmp.max_move_speed;
            rigidbody.velocity = new Vector3(gorizontal_speed.x, rigidbody.velocity.y, gorizontal_speed.y);
        }
    }

    void Rotate(int entity)
    {
        float sensitivity = Storage.GetComponent<MoveCmp>(entity).rotate_sensitivity_y;
        Vector3 MyAngle = new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        RigidbodyCmp rigidbodyCmp = Storage.GetComponent<RigidbodyCmp>(entity);
        rigidbodyCmp.rigidbody.MoveRotation(rigidbodyCmp.transform.rotation * Quaternion.Euler(MyAngle));
    }

    void Jump(int player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float jump_force = Storage.GetComponent<JumpCmp>(player).jump_force;
            Rigidbody rigidbody = Storage.GetComponent<RigidbodyCmp>(player).rigidbody;

            rigidbody.AddForce(0, jump_force, 0);
        }
    }

    public void SignalHandler(StartStopMoveSignal arg)
    {

        need_move = need_rotate = arg.signal_to_start;
    }
}
