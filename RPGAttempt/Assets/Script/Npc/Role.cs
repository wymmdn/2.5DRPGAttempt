using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour ,IInteraction,IAssailable
{
    [Header("attributes")]
    public int maxHealth;
    public int curHealth;
    public float physicResist;
    public float fireResist;
    public float moveSpeed;
    [Header("states")]
    public bool isAttacking;
    public bool isMoving;
    public bool isTalking;
    public bool isInvicible;
    public float invicibleTime;

    protected Weapon weapon;
    protected GameObject defaultWeapon;
    protected HealthBarStd healthBar;
    //protected Role attackTarget;
    [HideInInspector]public RolesAnimatorManager animatorManager;
    [HideInInspector]public float attackRadius;
    [HideInInspector]public Vector2 faceDir;   //标记角色的朝向
    protected Rigidbody2D rb;
    
    protected virtual void Awake()
    {
        animatorManager = GetComponent<RolesAnimatorManager>();
        rb = GetComponent<Rigidbody2D>();
        defaultWeapon = (GameObject)Resources.Load(GloblePath.defaultWeaponPath, typeof(GameObject));
        changeWeapon(Instantiate(defaultWeapon,transform));
        this.attackRadius = weapon.attackRadius;
        healthBar = GetComponentInChildren<HealthBarStd>();
        isInvicible = isAttacking = isMoving = isTalking = false;
    }
    public virtual void changeWeapon(GameObject wp)
    {
        if (wp.GetComponent<Weapon>() == null)
            return;
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = wp.GetComponent<Weapon>();
        }
        else {
            weapon = wp.GetComponent<Weapon>();
        }
        wp.transform.SetParent(this.transform);
        wp.transform.position = this.transform.position + weapon.positionOffset;
    }
    public virtual void changeHealth(int health, changeHealthType type)
    {
        switch (type)
        {
            case changeHealthType.heal:
                curHealth += health;
                animatorManager.getHealAnimation();
                break;
            case changeHealthType.physicDamage:
                if (!isInvicible)
                {
                    dealPhysicDamage(health);
                }
                break;
            case changeHealthType.fireDamage:
                if (!isInvicible)
                {
                    dealFireDamage(health);
                }
                break;
            default:
                if (!isInvicible)
                {
                    curHealth -= health;
                    animatorManager.getHurtAnimation();
                }
                break;
        }
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth < 0)
        {
            curHealth = 0;
        }
        healthBar?.healthDisplay((float)curHealth/maxHealth);
    }
    public void movement(Vector2 target)
    {
        if (!isAttacking)
        {
            faceDir = (target - (Vector2)transform.position).normalized;
            animatorManager.movingAnimation(faceDir);
            rb.velocity = faceDir * moveSpeed;
        }
    }
    public void stopMove()
    {
        faceDir = rb.velocity.normalized;
        rb.velocity = Vector2.zero;
        animatorManager.idleAnimation();
    }
    public virtual void toDead()
    {
        animatorManager.deadAnimation();
        this.transform.SetParent(null);
        Destroy(this.gameObject);
    }
    public void attack()
    {
        rb.velocity = Vector2.zero;
        weapon.attack();
    }
    protected virtual void dealPhysicDamage(int health)
    {
        health = (int)(health * (1 - physicResist));
        curHealth -= health;
        animatorManager.getHurtAnimation();
    }
    protected virtual void dealFireDamage(int health)
    {
        health = (int)(health * (1 - fireResist));
        curHealth -= health;
        animatorManager.getHurtAnimation();
    }
    public void attackStart()   //called in animator event
    { 
        isAttacking = true;
    }
    public void attackEnd()     
    {
        isAttacking = false;
    }

    public virtual void interact(Role role)
    {
        
    }
}
