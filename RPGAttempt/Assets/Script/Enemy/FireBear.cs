using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FireBearStates;

public class FireBear : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        idleState = new FireBearIdleState();
        chaseState = new FireBearChaseState();
        attackState = new FireBearAttackState();
        states.Add(stateType.idle, idleState);
        states.Add(stateType.chase, chaseState);
        states.Add(stateType.attack, attackState);
    }

    protected override void Start()
    {
        base.Start();
    }
    /*public override void toDead()
    {
        base.toDead();
        //EventHandler.CallFireBear(StoryManager.dead);
    }*/
}
