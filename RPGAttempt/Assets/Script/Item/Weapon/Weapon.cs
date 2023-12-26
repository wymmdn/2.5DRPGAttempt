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
    public float attackRadius;
    public Role master;
    protected WeaponAnimatorManager weaponAnimator;
    protected CircleCollider2D attackCol;
    protected List<Transform> attackTargets = new List<Transform>();
    protected Transform attackTarget;
    public Vector2 attackDir;

    private float timeCnt;   //攻击间隔计时，小于0时可以攻击

    protected virtual void Awake()
    {
        master = GetComponentInParent<Role>();
        weaponAnimator = GetComponent<WeaponAnimatorManager>();
        attackCol = this.gameObject.AddComponent<CircleCollider2D>();
        attackCol.radius = attackRadius;
        attackCol.isTrigger = true;
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

    public virtual void attack()
    {
        if (timeCnt <= 0)
        {
            var nearDis = float.MaxValue;
            attackDir = master.faceDir;
            foreach (Transform t in attackTargets)
            {
                if (Vector2.Distance((Vector2)t.position, (Vector2)this.transform.position) < nearDis)
                {
                    nearDis = Vector2.Distance((Vector2)t.position, (Vector2)this.transform.position);
                    attackDir = ((Vector2)t.position - (Vector2)this.transform.position).normalized;
                    attackTarget = t;
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
