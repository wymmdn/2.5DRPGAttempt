using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeMonsterStates;
using ModelMgr;

public class TreeMonster : Enemy
{

    protected override void Awake()
    {
        idleState = new TreeMonsterIdleState();
        chaseState = new TreeMonsterChaseState();
        states.Add(stateType.idle, idleState);
        states.Add(stateType.chase,chaseState);
    }

    protected override void Start()
    {
        base.Start();
    }
}
