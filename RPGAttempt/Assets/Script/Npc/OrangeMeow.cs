using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrangeMeowStates;
using UnityEngine.UI;

public class OrangeMeow : NPC
{
    [SerializeField] private Text shortWords;
    private NPCState idleState;
    private NPCState runAwayState;

    protected override void Awake()
    {
        base.Awake();
        idleState = new OrangeMeowIdleState();
        runAwayState = new OrangeMeowRunState();
        states.Add(stateType.idle, idleState);
        states.Add(stateType.run, runAwayState);
        bornPoint = transform.position;
    }
    private void OnEnable()
    {
        currentState = idleState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        rb.velocity = Vector2.zero;
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

}
