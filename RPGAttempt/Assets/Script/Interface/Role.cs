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
    [HideInInspector]protected Animator anim;
    protected Rigidbody2D rb;
    public Vector2 faceDir;   //标记角色的朝向

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        changeWeapon((GameObject)Resources.Load(Path.defaultWeaponPath,typeof(GameObject)));
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
                anim.SetTrigger("getHeal");
                break;
            case changeHealthType.damage:
                if (!isInvicible)
                {
                    curHealth -= health;
                    anim.SetTrigger("getHurt");
                }
                break;
            default:
                if (!isInvicible)
                {
                    curHealth -= health;
                    anim.SetTrigger("getHurt");
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
        healthBar.healthDisplay((float)curHealth/maxHealth);
    }

    

    public void attack()
    {

    }
}
