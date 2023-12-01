using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

namespace TreeMonsterStates
{
    public class TreeMonsterIdleState : EnemyState
    {
        public override void OnEnter(Enemy enemy)
        {
            currentEnemy = enemy;
        }
        public override void LogicUpdate()
        {
            if (currentEnemy.foundPlayer()) 
            {
                currentEnemy.TransitionState(stateType.chase);
            }
        }        

        public override void PhysicsUpdate()
        {
            
        }
        public override void OnExit()
        {

        }
    }

    public class TreeMonsterChaseState : EnemyState
    {
        public override void OnEnter(Enemy enemy)
        {
            Debug.Log("chase");
            currentEnemy = enemy;
            currentEnemy.anim.SetBool("isMoving", true);
        }
        public override void LogicUpdate()
        {
            currentEnemy.movement(currentEnemy.player.transform.position);
        }

        public override void PhysicsUpdate()
        {

        }
        public override void OnExit()
        {

        }


    }


}

