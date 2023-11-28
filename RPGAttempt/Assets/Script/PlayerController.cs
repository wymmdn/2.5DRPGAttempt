using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public int maxHeart;
    public int curHeart;
    public HealthController healthController;
    private bool isInvicible;
    private float invicibleTime;
    private float invicibleTimeCnt;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        curHeart = maxHeart = 5;
        invicibleTime = 1.0f;
        invicibleTimeCnt = invicibleTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttack();
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
                    Debug.Log("damaged");
                    curHeart -= health;
                    isInvicible = true;
                }
                break ;
            default:
                if (!isInvicible)
                {
                    curHeart -= health;
                    isInvicible = true;
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
