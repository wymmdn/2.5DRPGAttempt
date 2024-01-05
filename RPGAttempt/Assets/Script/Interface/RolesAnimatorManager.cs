using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolesAnimatorManager : MonoBehaviour
{
    private Animator anim;
    private Role role;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        role = GetComponent<Role>();
        AnimationEvent attackStart = new AnimationEvent();
        AnimationEvent attackEnd = new AnimationEvent();
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.StartsWith("Attack") || clip.name.StartsWith("attack"))
            {
                attackStart.functionName = "attackStart";   // set Role.isAttacking to true
                attackEnd.functionName = "attackEnd";       // set Role.isAttacking to false
                attackStart.time = 0f;
                attackEnd.time = clip.length;
                clip.AddEvent(attackStart);
                clip.AddEvent(attackEnd);
                Debug.Log("added");
            }
        }
    }
    public void movingAnimation(Vector2 dir)
    {
        anim.SetBool("isMoving", true);
        anim.SetFloat("dirX", dir.x);
        anim.SetFloat("dirY", dir.y);
    }
    public void idleAnimation()
    {
        anim.SetBool("isMoving", false);
        //anim.SetFloat("dirX", dir.x);
        //anim.SetFloat("dirY", dir.y);
    }
    public void attackAnimation(Vector2 dir)
    {
        anim.SetTrigger("attack");
        anim.SetFloat("dirX", dir.x);
        anim.SetFloat("dirY", dir.y);
    }
    public void getHurtAnimation()
    {
        anim.SetTrigger("getHurt");
    }
    public void getHealAnimation()
    {
        anim.SetTrigger("getHeal");
    }
    public void deadAnimation()
    { 
    
    }
    public void attackStart()   //called in animator event
    {
        role.isAttacking = true;
    }
    public void attackEnd()
    {
        role.isAttacking = false;
        Debug.Log("called");
    }
}
