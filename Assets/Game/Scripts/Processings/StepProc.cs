using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepProc : ProcessingBase, ICustomUpdate
{
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerStepsCmp, FPSCmp>());

    public void CustomUpdate()
    {
        foreach (int player in PlayerGroup)
        {
            StepSound(player);
        }
        
    }


    void StepSound(int player)
    {
        //int player = PlayerGroup.GetEntitiesArray()[0];
        //Vector3 velocity = Storage.GetComponent<RigidbodyCmp>(player).rigidbody.velocity;
        //Vector2 gorisontal_velocity = new Vector2(velocity.x, velocity.z);
        AudioSource audioSource = Storage.GetComponent<PlayerStepsCmp>(player).audioSource;
        bool is_moving = Storage.GetComponent<FPSCmp>(player).is_moving;
        bool is_grounded = Storage.GetComponent<FPSCmp>(player).controller.isGrounded;



        if (is_moving && is_grounded)
        {
            //Debug.Log("музыка On");
            if (!audioSource.isPlaying)
                audioSource.Play();

            if (audioSource.volume < 0.1)
                audioSource.volume = 1;
        }
        else if (audioSource.isPlaying)
        {
            audioSource.volume = 0;
        }
        
    }
}
