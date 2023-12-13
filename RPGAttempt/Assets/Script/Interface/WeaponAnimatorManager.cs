using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class WeaponAnimatorManager : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void attackAnimation(Vector2 dir)
    {
        anim.SetTrigger("attack");
        anim.SetFloat("dirX", dir.x);
        anim.SetFloat("dirY", dir.y);
    }

    public void changeAttackSpeed(float playSpeed)
    {
        bool changeFlag = false;
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name.StartsWith("Attack") || clip.name.StartsWith("attack"))
            {
                if (playSpeed < clip.length)
                {
                    changeFlag = true;
                }
            }
        }
        if (changeFlag == false) return;

        AnimatorControllerLayer[] animLayers = ((AnimatorController)anim.runtimeAnimatorController).layers;
        foreach (AnimatorControllerLayer ly in animLayers)
        {
            foreach (ChildAnimatorState cs in ly.stateMachine.states)
            {
                if (cs.state.name == "Attack" || cs.state.name == "attack")
                { 
                    cs.state.speed = playSpeed;
                }
            }
        }
    }
}
