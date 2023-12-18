using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeMonsterStates;
using ModelMgr;

public class TreeMonster : Enemy
{

    protected override void Awake()
    {
        base.Awake();
        idleState = new TreeMonsterIdleState();
        chaseState = new TreeMonsterChaseState();
        attackState = new TreeMonsterAttackState();
        states.Add(stateType.idle, idleState);
        states.Add(stateType.chase,chaseState);
        states.Add(stateType.attack, attackState);
    }

    protected override void Start()
    {
        base.Start();
    }
    public override void toDead()
    {
        base.toDead();
        EventHandler.CallTreeMonster(StoryManager.dead);
    }
}
