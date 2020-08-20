using RangerV;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProc : ProcessingBase, ICustomFixedUpdate
{
    Group MoveGroup = Group.Create(new ComponentsList<PlayerCmp, MoveCmp, RigidbodyCmp>());

    public void CustomFixedUpdate()
    {
        foreach (int move in MoveGroup)
        {
            EntityBase entity = EntityBase.GetEntity(move);

            Move(entity);
            Rotate(entity);
            //Debug.Log("DDD");
        }
    }


    void Move(EntityBase entity)
    {
        MoveCmp moveCmp = entity.GetEntityComponent<MoveCmp>();
        Rigidbody rigidbody = entity.GetEntityComponent<RigidbodyCmp>().rigidbody;
        MoveControls moveControls = moveCmp.moveControls;

        if (Input.GetKey(moveControls.Forvard))
            rigidbody.AddRelativeForce(new Vector3(moveCmp.move_speed, 0, 0));
        else if (Input.GetKey(moveControls.Backward))
            rigidbody.AddRelativeForce(new Vector3(-moveCmp.move_speed, 0, 0));

        if (Input.GetKey(moveControls.Left))
            rigidbody.AddRelativeForce(new Vector3(0, 0, moveCmp.move_speed));
        else if (Input.GetKey(moveControls.Rigt))
            rigidbody.AddRelativeForce(new Vector3(0, 0, -moveCmp.move_speed));


        Vector2 gorizontal_speed = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);

        if (gorizontal_speed.magnitude > moveCmp.max_speed)
        {
            gorizontal_speed = gorizontal_speed.normalized * moveCmp.max_speed;
            rigidbody.velocity = new Vector3(gorizontal_speed.x, rigidbody.velocity.y, gorizontal_speed.y);
        }
    }

    void Rotate(EntityBase entity)
    {

    }

}
