using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class PlayerController : MonoBehaviour
{
    public int maxHeart;
    public int curHeart;
    public float moveSpeed;
    public bool isInvicible;
    public float invicibleTime;

    [HideInInspector]public HealthController healthController;
    private float invicibleTimeCnt;
    private Animator anim;
    private Rigidbody2D rigidbody2d;
    private float inputX, inputY;
    private float stopX, stopY;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //curHeart = maxHeart = 5;
        invicibleTime = 1.0f;
        invicibleTimeCnt = invicibleTime;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthController.healthDisplay(curHeart);
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
                Debug.Log(isInvicible.ToString() + invicibleTimeCnt.ToString());
            }
        }
        if (curHeart == 0)
        { 
            GameManager.instance.GameOver();
        }
    }

    public void CheckAttack()
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
    public void CheckMovement()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector2 input = (transform.right * inputX + transform.up * inputY).normalized;
        rigidbody2d.velocity = input * moveSpeed;

        if (input != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
            stopX = inputX;
            stopY = inputY;
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        anim.SetFloat("InputX", stopX);
        anim.SetFloat("InputY", stopY);
    }
    public void changeHealth(int health, changeHealthType type)
    {
        switch (type)
        {
            case changeHealthType.heal:
                curHeart += health;
                break;
            case changeHealthType.damage:
                if (!isInvicible)
                {
                    curHeart -= health;
                    isInvicible = true;
                    anim.SetTrigger("getHurt");
                }
                break ;  
            default:
                if (!isInvicible)
                {
                    curHeart -= health;
                    isInvicible = true;
                    anim.SetTrigger("getHeal");
                }
                break;
        }

        if (curHeart > maxHeart)
        {
            curHeart = maxHeart;
        }
        if (curHeart < 0)
        { 
            curHeart = 0;
        }
        healthController.healthDisplay(curHeart);
        Debug.Log("health change to " + curHeart.ToString());
    }
    public void PhysicCheck()
    { 
        //RaycastHit2D
    }
}
