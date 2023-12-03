using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class Enemy : MonoBehaviour        //暂定所有的敌人空闲时原地不动，发现玩家时开始追逐并攻击，脱战时回到据点
{
    public int maxHealth;
    public int minHealth;
    public int moveSpeed;
    public bool isInvicible;
    public float invicibleTime;
    public float alertRadius;
    public float attackRadius;
    public GameObject player;

    public Vector3 bornPoint;
    protected EnemyState idleState;
    protected EnemyState chaseState;
    protected EnemyState attackState;
    protected EnemyState currentState;
    [HideInInspector]public Dictionary<stateType, EnemyState> states = new Dictionary<stateType, EnemyState>();

    [HideInInspector]public Animator anim;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        currentState = idleState;
        currentState.OnEnter(this);
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        bornPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }
    public void attack()
    { 
        
    }
    public void movement(Vector2 target) {
        target = (target - (Vector2)transform.position).normalized;
        anim.SetBool("isMoving", true);
        anim.SetFloat("dirX", target.x);
        anim.SetFloat("dirY", target.y);
        rb.velocity = target * moveSpeed;
    }
    public void stopMove() { 
        Vector2 stop = rb.velocity.normalized;
        movement(transform.position);
        anim.SetBool("isMoving", false);
        anim.SetFloat("dirX", stop.x);
        anim.SetFloat("dirY", stop.y);
    }

    public bool foundArea(Vector2 target) 
    {
        //Debug.Log((player.transform.position - transform.position).sqrMagnitude);
        return alertRadius > (target - (Vector2)transform.position).sqrMagnitude ? true : false;
    }
    public bool attackArea(Vector2 target) 
    {
        return attackRadius > (target - (Vector2)transform.position).sqrMagnitude ? true : false;
    }

    public void TransitionState(stateType type)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)this.transform.position,alertRadius);
        Gizmos.DrawWireSphere((Vector2)this.transform.position, attackRadius);
    }
}
