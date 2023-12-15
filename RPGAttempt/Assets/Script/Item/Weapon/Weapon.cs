using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class Weapon : MonoBehaviour
{
    public int damage;
    public changeHealthType damageType;
    //public float attackSpeed;
    public float attackInterval;
    public float attackIntervalInit;
    public Role master;
    protected WeaponAnimatorManager weaponAnimator;
    [HideInInspector]public List<DamageField> bullets;
    
    private float timeCnt;   //攻击间隔计时，小于0时可以攻击

    protected virtual void Awake()
    {
        master = GetComponentInParent<Role>();
        weaponAnimator = GetComponent<WeaponAnimatorManager>();
        attackIntervalInit = attackInterval = 1;
        //attackSpeed = 1 / attackInterval;
        timeCnt = -0.1f;
    }

    protected virtual void Update()
    {
        if (timeCnt >= 0)
        { 
            timeCnt -= Time.deltaTime;
        }
    }

    public void attack()
    {
        if (timeCnt <= 0)
        {
            master.animatorManager.attackAnimation(master.faceDir);
            weaponAnimator.attackAnimation(master.faceDir);
            timeCnt = attackInterval;
        }
    }
    public void changeDamage(int damege)
    { 
        this.damage = damege;
    }
    public void changeSpeed(float interval)
    { 
        this.attackInterval = interval;
        weaponAnimator.changeAttackSpeed(attackInterval);
    }

    private float GetAnimatorLength(Animator animator, string name)
    {
        //动画片段时间长度
        float length = 0;

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        Debug.Log(clips.Length);
        foreach (AnimationClip clip in clips)
        {
            Debug.LogError(clip.name + "  " + clip.length);
            if (clip.name.Equals(name))
            {
                length = clip.length;
                break;
            }
        }
        return length;
    }

}
