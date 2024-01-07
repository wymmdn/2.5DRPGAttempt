using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item,IInteraction
{
    public int damage;
    public changeHealthType damageType;
    public float attackInterval;
    public float attackIntervalInit;
    public float attackRadius;
    public Vector3 positionOffset;  //武器装备在身上的位置，默认是0
    public Role master;
    protected WeaponAnimatorManager weaponAnimator;
    protected CircleCollider2D attackCol;
    protected List<Transform> attackTargets = new List<Transform>();
    protected Transform attackTarget;
    public Vector2 attackDir;

    private float timeCnt;   //攻击间隔计时，小于0时可以攻击

    protected override void Awake()
    {
        base.Awake();
        interactOpts.Insert(0, UIString.equip);
        master = GetComponentInParent<Role>();
        weaponAnimator = GetComponent<WeaponAnimatorManager>();
        attackCol = this.gameObject.AddComponent<CircleCollider2D>();
        attackCol.radius = attackRadius;
        attackCol.isTrigger = true;
        positionOffset = Vector3.zero;
        timeCnt = -0.1f;
    }

    protected virtual void Update()
    {
        if (timeCnt >= 0)
        { 
            timeCnt -= Time.deltaTime;
        }
    }

    public virtual void attack()
    {
        if (timeCnt <= 0)
        {
            var nearDis = float.MaxValue;
            attackDir = master.faceDir;
            if (master.tag == tagtag.enemy || master.tag == tagtag.npc)
            {
                attackTarget = GameObject.FindGameObjectWithTag(tagtag.player).GetComponent<Transform>();
                attackDir = ((Vector2)attackTarget.position - (Vector2)transform.position).normalized;
            }
            else
            { 
                foreach (Transform t in attackTargets)
                {
                    if (Vector2.Distance((Vector2)t.position, (Vector2)this.transform.position) < nearDis)
                    {
                        nearDis = Vector2.Distance((Vector2)t.position, (Vector2)this.transform.position);
                        attackDir = ((Vector2)t.position - (Vector2)this.transform.position).normalized;
                        attackTarget = t;
                    }
                }
            }
            playAttack();
            timeCnt = attackInterval;
        }
    }
    protected virtual void playAttack()
    { 
        master.animatorManager.attackAnimation(attackDir);
        weaponAnimator.attackAnimation(attackDir);
    }
    public void interact(Role role)
    {
        if (this.master != null || this.holder != null)
            return;
        equip(role);
    }
    public virtual void equip(Role master)
    {
        master.equipWeapon(this);
        this.transform.SetParent(master.transform);
        this.transform.position = master.transform.position + positionOffset;
        this.master = master;
        this.isPickable = false;
    }
    public virtual void unEquip() 
    {
        if (master == null) return;
        this.transform.SetParent(master.transform.parent);
        if (master.itemBag != null)
        {
            this.pickUp(master);
        }
        this.master = null;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        IAssailable obj = other.GetComponent<IAssailable>();
        if (!attackTargets.Contains(other.transform) && obj != null) { attackTargets.Add(other.transform); }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        attackTargets.Remove(other.transform);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)this.transform.position, attackRadius);
    }

    
}
