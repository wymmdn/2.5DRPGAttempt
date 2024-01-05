using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour ,IInteraction,IAssailable
{
    [Header("attributes")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int curHealth;
    [SerializeField] protected float physicResist;
    [SerializeField] protected float fireResist;
    [SerializeField] protected float moveSpeed;
    [Header("states")]
    [SerializeField] protected bool isMoving;
    [SerializeField] protected bool isTalking;
    [SerializeField] protected bool isInvicible;
    [SerializeField] protected float invicibleTime;
    [SerializeField] public bool isAttacking;
    

    public ItemBag itemBag;
    [HideInInspector] public Weapon weapon;
    [HideInInspector] public GameObject defaultWeapon;
    [HideInInspector] public RolesAnimatorManager animatorManager;
    [HideInInspector] public float attackRadius;
    [HideInInspector] public float interactRadius;
    [HideInInspector] public Vector2 faceDir;   //标记角色的朝向
    protected Rigidbody2D rb;
    protected HealthBarStd healthBar;
    
    protected virtual void Awake()
    {
        animatorManager = GetComponent<RolesAnimatorManager>();
        rb = GetComponent<Rigidbody2D>();
        defaultWeapon = (GameObject)Resources.Load(GloblePath.defaultWeaponPath);
        Instantiate(defaultWeapon).GetComponent<Weapon>().equip(this);
        //defaultWeapon.equip(this);
        this.attackRadius = weapon.attackRadius;
        healthBar = GetComponentInChildren<HealthBarStd>();
        isInvicible = isAttacking = isMoving = isTalking = false;
    }
    public virtual void equipWeapon(Weapon wp)
    {
        if (weapon != null)
        {
            weapon.unEquip();
        }
        weapon = wp;
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

    public virtual void interact(Role role)
    {
        
    }
}
