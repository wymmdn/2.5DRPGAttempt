using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class PlayerController : Role
{
    [Header("Init in inspector")]
    public HealthBarPlayer healthBarPlayer;  
    public Equipments equipments;


    private float invicibleTimeCnt;
    private float inputX, inputY;
    [SerializeField]private List<Collider2D> interactCols = new List<Collider2D>(); //存储所有进入trigger collider的collider，用于交互判断


    protected override void Awake()
    {
        base.Awake();
        //changeWeapon((GameObject)Resources.Load(GloblePath.fireWand, typeof(GameObject)));
        //curHeart = maxHeart = 5;
        invicibleTime = 0.75f;
        invicibleTimeCnt = invicibleTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        healthBarPlayer.healthDisplay(curHealth);
    }
    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += talkStart;
        EventHandler.CloseDialogueEvent += talkEnd;
    }
    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= talkStart;
        EventHandler.CloseDialogueEvent -= talkEnd;
    }
    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            isInvicible = true;
            return;
        }
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
                animatorManager.idleAnimation();
            }
        }
    }
    private void checkInteract()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float distance = float.MaxValue;
            Collider2D nearstCol = new Collider2D();
            foreach (Collider2D col in interactCols)      //可以优化，一个物体有多个collider，存transform可以减少遍历次数
            {
                if (Vector2.Distance((Vector2)col.transform.position, (Vector2)this.transform.position) < distance)
                { 
                    nearstCol = col;
                    distance = Vector2.Distance((Vector2)nearstCol.transform.position, (Vector2)this.transform.position);
                }
            }
            IInteraction interaction = nearstCol.transform.parent.GetComponent<IInteraction>();
            if (interaction == null)
                interaction = nearstCol.GetComponent<IInteraction>();
            interaction?.interact(this);
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
            case changeHealthType.physicDamage:
            case changeHealthType.fireDamage:
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
    public override void changeWeapon(GameObject wp)
    {
        base.changeWeapon(wp);
        equipments.displayWeapon(wp.GetComponent<Weapon>());
    }

    private void talkStart(Conversation c)
    {
        this.isTalking = true;
    }
    private void talkEnd()
    {
        this.isTalking = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IInteraction>() != null && !interactCols.Contains(other)) { interactCols.Add(other); }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interactCols.Remove(other);
    }
    public void PhysicCheck()
    { 
        //RaycastHit2D
    }
}
