using RangerV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepProc : ProcessingBase, ICustomUpdate
{
    Group PlayerGroup = Group.Create(new ComponentsList<PlayerStepsCmp, RigidbodyCmp>());

    public void CustomUpdate()
    {
        StepSound();
    }


    void StepSound()
    {
        int player = PlayerGroup.GetEntitiesArray()[0];
        Vector3 velocity = Storage.GetComponent<RigidbodyCmp>(player).rigidbody.velocity;
        Vector2 gorisontal_velocity = new Vector2(velocity.x, velocity.z);
        AudioSource audioSource = Storage.GetComponent<PlayerStepsCmp>(player).audioSource;

        if (gorisontal_velocity.magnitude > 0.1f)
        {
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
