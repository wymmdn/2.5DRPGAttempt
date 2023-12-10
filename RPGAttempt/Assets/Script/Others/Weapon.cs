using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class Weapon : MonoBehaviour
{
    public int damage;
    public changeHealthType damageType;
    public float attackSpeed;
    public float attackInterval;
    public Role master;
    [HideInInspector]public Animator anim;
    [HideInInspector]public List<DamageField> bullets;
    
    private float timeCnt; 

    protected virtual void Awake()
    {
        master = GetComponentInParent<Role>();
        anim = GetComponent<Animator>();
        attackSpeed = 1;
        attackInterval = 1 / attackSpeed;
        timeCnt = -0.1f;
    }

    protected virtual void Update()
    {
        if (timeCnt >= 0)
        { 
            timeCnt -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timeCnt <= 0)
        {
            master.attack();
            //this.attack
            timeCnt = attackInterval;
        }
    }

}
