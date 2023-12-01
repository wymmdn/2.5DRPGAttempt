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
    public GameObject player;

    protected Vector3 bornPoint;
    protected EnemyState idleState;
    protected EnemyState chaseState;
    protected EnemyState currentState;
    [HideInInspector]public Dictionary<stateType, EnemyState> states = new Dictionary<stateType, EnemyState>();

    [HideInInspector]public Animator anim;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public void movement(Vector2 target) { 
        
    }

    public bool foundPlayer() 
    {
        //Debug.Log((player.transform.position - transform.position).sqrMagnitude);
        return alertRadius > (player.transform.position - transform.position).sqrMagnitude ? true : false;
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
    }
}
