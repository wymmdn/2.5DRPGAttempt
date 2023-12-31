using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeMonsterStates;

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
        this.weapon.attackInterval = 1.5f;
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
