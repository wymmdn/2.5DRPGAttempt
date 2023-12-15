using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class PlayerController : Role
{

    public HealthBarPlayer healthBarPlayer;  //��inspector�и�ֵ��
    private float invicibleTimeCnt;
    private float inputX, inputY;
    [SerializeField]private List<Collider2D> colliders = new List<Collider2D>(); //�洢���н���trigger collider��collider�����ڽ����ж�


    protected override void Awake()
    {
        base.Awake();
        //curHeart = maxHeart = 5;
        invicibleTime = 0.75f;
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
        checkAttack();
        checkMovement();
        checkInteract();
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

    private void checkAttack()
    {
        if (Input.GetKey(KeyCode.F))
        {
            rb.velocity = Vector2.zero;
            weapon.attack();
        }
    }
    private void checkMovement()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector2 input = (transform.right * inputX + transform.up * inputY).normalized;
        if (!isAttacking)
        { 
            rb.velocity = input * moveSpeed;
            if (input != Vector2.zero)
            {
                faceDir = input;
                animatorManager.movingAnimation(faceDir);
            }
            else
            {
                animatorManager.idleAnimation(faceDir);
            }
        }
    }
    private void checkInteract()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float distance = 999f;
            Collider2D nearstCol = new Collider2D();
            foreach (Collider2D col in colliders)      //�����Ż���һ�������ж��collider����transform���Լ��ٱ�������
            {
                if (Vector2.Distance((Vector2)col.transform.position, (Vector2)this.transform.position) < distance)
                { 
                    nearstCol = col;
                }
            }
            IInteraction interaction = nearstCol.transform.GetComponent<IInteraction>();
            interaction?.interact();
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        colliders.Remove(other);
    }
    public void PhysicCheck()
    { 
        //RaycastHit2D
    }
}
