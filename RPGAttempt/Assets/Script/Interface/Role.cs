using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class Role : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public float moveSpeed;
    public bool isInvicible;
    public float invicibleTime;
    protected Weapon weapon;
    protected HealthBarStd healthBar;
    protected Animator anim;
    protected RolesAnimatorManager animatorManager;
    protected Rigidbody2D rb;
    public Vector2 faceDir;   //标记角色的朝向

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        animatorManager = GetComponent<RolesAnimatorManager>();
        rb = GetComponent<Rigidbody2D>();
        changeWeapon((GameObject)Resources.Load(GloblePath.defaultWeaponPath,typeof(GameObject)));
        healthBar = GetComponentInChildren<HealthBarStd>();
    }
    public virtual void changeWeapon(GameObject wp)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = Instantiate(wp, transform).GetComponent<Weapon>();
        }
        else {
            weapon = Instantiate(wp, transform).GetComponent<Weapon>();
        }
    }
    public virtual void changeHealth(int health, changeHealthType type)
    {
        switch (type)
        {
            case changeHealthType.heal:
                curHealth += health;
                animatorManager.getHealAnimation();
                break;
            case changeHealthType.damage:
                if (!isInvicible)
                {
                    curHealth -= health;
                    animatorManager.getHurtAnimation();
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
        faceDir = (target - (Vector2)transform.position).normalized;
        animatorManager.movingAnimation(faceDir);
        rb.velocity = faceDir * moveSpeed;
    }
    public void stopMove()
    {
        faceDir = rb.velocity.normalized;
        rb.velocity = Vector2.zero;
        animatorManager.idleAnimation(faceDir);
    }

    public void attack()
    {

    }
}
