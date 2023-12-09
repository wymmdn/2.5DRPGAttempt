using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class Weapon : MonoBehaviour
{
    public int damage;
    public changeHealthType damageType;
    public Animator anim;
    public List<DamageField> bullets;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("attack", true);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            anim.SetBool("attack", false);
        }
    }

}
