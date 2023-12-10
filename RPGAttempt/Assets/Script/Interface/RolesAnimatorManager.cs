using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolesAnimatorManager : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void movingAnimation(Vector2 dir)
    {
        anim.SetBool("isMoving", true);
        anim.SetFloat("dirX", dir.x);
        anim.SetFloat("dirY", dir.y);
    }
    public void idleAnimation(Vector2 dir)
    {
        anim.SetBool("isMoving", false);
        anim.SetFloat("dirX", dir.x);
        anim.SetFloat("dirY", dir.y);
    }
    public void attackAnimation()
    { 
    
    }
    public void getHurtAnimation()
    {
        anim.SetTrigger("getHurt");
    }
    public void getHealAnimation()
    {
        anim.SetTrigger("getHeal");
    }

}
