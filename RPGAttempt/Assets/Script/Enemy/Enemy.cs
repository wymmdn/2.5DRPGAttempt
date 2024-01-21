using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Role        //�ݶ����еĵ��˿���ʱԭ�ز������������ʱ��ʼ׷�𲢹�������սʱ�ص��ݵ�
{
    public float alertRadius;
    public GameObject player;  //��ʱ����state����player��λ��
    public Vector3 bornPoint;
    protected EnemyState idleState;
    protected EnemyState chaseState;
    protected EnemyState attackState;
    protected EnemyState currentState;
    [HideInInspector]public Dictionary<stateType, EnemyState> states = new Dictionary<stateType, EnemyState>();


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
        if (isDead) 
        {
            return;        
        }
        if (curHealth == 0)
        {
            toDead();
        }
        currentState.LogicUpdate();
        UpdateContent();
    }
    protected virtual void UpdateContent()
    { 
        
    }
    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }

    public bool foundArea(Vector2 target) 
    {
        return alertRadius > Vector2.Distance(target, (Vector2)transform.position) ? true : false;
    }
    public bool attackArea(Vector2 target) 
    {
        return attackRadius > Vector2.Distance(target, (Vector2)transform.position) ? true : false;
    }
    public bool inDistance(Vector2 target, float distance)
    {
        return distance > Vector2.Distance(target, (Vector2)transform.position) ? true : false;
    }
    public void TransitionState(stateType type)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter(this);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)this.transform.position,alertRadius);
        Gizmos.DrawWireSphere((Vector2)this.transform.position, attackRadius);
    }

}
