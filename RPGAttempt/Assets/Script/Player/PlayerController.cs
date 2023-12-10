using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class PlayerController : Role
{

    public HealthBarPlayer healthBarPlayer;  //在inspector中赋值的
    private float invicibleTimeCnt;
    private float inputX, inputY;


    protected override void Awake()
    {
        base.Awake();
        //curHeart = maxHeart = 5;
        invicibleTime = 1.0f;
        invicibleTimeCnt = invicibleTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        healthBarPlayer.healthDisplay(curHealth);
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttack();
        CheckMovement();
        if (isInvicible)
        {
            invicibleTimeCnt -= Time.deltaTime;
            
            if (invicibleTimeCnt <= 0.0f)
            { 
                isInvicible = false;
                invicibleTimeCnt = invicibleTime;
            }
        }
        if (curHealth == 0)
        { 
            GameManager.instance.GameOver();
        }
    }

    public void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("attack", true);
            weapon.anim.SetFloat("dirX", faceDir.x);
            weapon.anim.SetFloat("dirY", faceDir.y);
            weapon.anim.SetBool("attack", true);
            //base.attack();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            anim.SetBool("attack", false);
            weapon.anim.SetBool("attack", false);
        }
    }
    public void CheckMovement()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector2 input = (transform.right * inputX + transform.up * inputY).normalized;
        rb.velocity = input * moveSpeed;

        if (input != Vector2.zero)
        {
            faceDir = input.normalized;
            animatorManager.movingAnimation(faceDir);
        }
        else
        {
            animatorManager.idleAnimation(faceDir);
        }
    }
    public override void changeHealth(int health, changeHealthType type)
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
                    isInvicible = true;
                    animatorManager.getHurtAnimation();
                }
                break ;  
            default:
                if (!isInvicible)
                {
                    curHealth -= health;
                    isInvicible = true;
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
        healthBarPlayer.healthDisplay(curHealth);
    }
    public void PhysicCheck()
    { 
        //RaycastHit2D
    }
}
