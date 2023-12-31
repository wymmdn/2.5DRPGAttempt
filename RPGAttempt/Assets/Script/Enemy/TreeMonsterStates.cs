using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreeMonsterStates
{
    public class TreeMonsterIdleState : EnemyState     ///////IDLE
    {
        public override void OnEnter(Enemy enemy)
        {
            currentEnemy = enemy;
        }
        public override void LogicUpdate()
        {
            if (currentEnemy.foundArea(currentEnemy.player.transform.position))
            {
                currentEnemy.TransitionState(stateType.chase);
            }
        }        

        public override void PhysicsUpdate()
        {
            if (currentEnemy.attackArea(currentEnemy.bornPoint) == false)
            {
                currentEnemy.movement(currentEnemy.bornPoint);
            }
            else
            {
                currentEnemy.stopMove();
            }
        }
        public override void OnExit()
        {

        }

    }

    public class TreeMonsterChaseState : EnemyState   /////CHASE
    {
        public override void OnEnter(Enemy enemy)
        {
            currentEnemy = enemy;
        }
        public override void LogicUpdate()
        {
            if (currentEnemy.attackArea(currentEnemy.player.transform.position))
            {
                currentEnemy.TransitionState(stateType.attack);
            }
            else if (currentEnemy.foundArea(currentEnemy.player.transform.position) == false)
            {
                currentEnemy.TransitionState(stateType.idle);
            }
            else
            {
                currentEnemy.movement(currentEnemy.player.transform.position);
            }
        }

        public override void PhysicsUpdate()
        {

        }
        public override void OnExit()
        {
            currentEnemy.stopMove();
        }


    }
    public class TreeMonsterAttackState : EnemyState
    {

        public override void OnEnter(Enemy enemy)
        {
            currentEnemy = enemy;
        }
        public override void LogicUpdate()
        {
            if (currentEnemy.attackArea(currentEnemy.player.transform.position))
            {
                currentEnemy.attack();
            }
            else if (currentEnemy.foundArea(currentEnemy.player.transform.position) &&
                     (currentEnemy.attackArea(currentEnemy.player.transform.position) == false))
            {
                currentEnemy.TransitionState(stateType.chase);
            }
            else
            {
                currentEnemy.TransitionState(stateType.idle);
            }
        }

        public override void PhysicsUpdate()
        {

        }
        public override void OnExit()
        {

        }
    }

}

