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
        foreach (int player in MoveGroup)
        {
            EntityBase playerEntity = EntityBase.GetEntity(player);

            Move(playerEntity);
            Rotate(playerEntity);
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

    void Rotate(EntityBase Player)
    {
        Vector3 MousePos = Input.mousePosition;

        Vector3 MyAngle = new Vector3();
        float sensitivity = Player.GetEntityComponent<MoveCmp>().rotate_sensitivity_y;
        // расчитываем угол, как:
        // разница между позицией мышки и центром экрана, делённая на размер экрана
        //  (чем дальше от центра экрана тем сильнее поворот)
        // и умножаем угол на чуствительность из параметров

        MyAngle = new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        //MyAngle = new Vector3(0, Input.GetAxis("Mouse X") * sensitivity.y, Input.GetAxis("Mouse Y") * sensitivity.z);
        //Debug.Log(Input.GetAxis("Vertical"));

        /*MyAngle.y = sensitivity.y * ((MousePos.x - (Screen.width / 2)) / Screen.width);
        MyAngle.z = sensitivity.x * ((MousePos.y - (Screen.height / 2)) / Screen.height);
        MyAngle.x = 0;*/

        RigidbodyCmp rigidbodyCmp = Storage.GetComponent<RigidbodyCmp>(Player.entity);
        rigidbodyCmp.rigidbody.MoveRotation(rigidbodyCmp.transform.rotation * Quaternion.Euler(MyAngle));
        //Debug.Log("fdasf");
        //rigidbodyCmp.rigidbody.
    }

}
