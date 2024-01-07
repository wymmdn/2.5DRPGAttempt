using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireBearStates
{
    public class FireBearIdleState : EnemyState     ///////IDLE
    {
        public override void OnEnter(Enemy enemy)
        {
            currentEnemy = enemy;
        }
        public override void LogicUpdate()
        {
            if (currentEnemy.foundArea(currentEnemy.player.transform.position))
            {
                currentEnemy.TransitionState(stateType.chase1);
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

    public class FireBearChase1State : EnemyState   /////CHASE
    {
        public FireBear fireBear;
        public override void OnEnter(Enemy enemy)
        {
            currentEnemy = enemy;
            fireBear = currentEnemy as FireBear;
        }
        public override void LogicUpdate()
        {
            if (fireBear.inDistance(fireBear.player.transform.position,fireBear.secondAttackRadius))
            {
                fireBear.TransitionState(stateType.secondAttack);
            }
            else if (fireBear.inDistance(fireBear.player.transform.position,fireBear.chaseRadius) == false)
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
    public class FireBearChase2State : EnemyState   /////CHASE
    {
        FireBear fireBear;
        public override void OnEnter(Enemy enemy)
        {
            currentEnemy = enemy;
            fireBear = currentEnemy as FireBear;
        }
        public override void LogicUpdate()
        {
            if (currentEnemy.attackArea(currentEnemy.player.transform.position))
            {
                currentEnemy.TransitionState(stateType.attack);
            }
            else if (fireBear.inDistance(fireBear.player.transform.position, fireBear.closeRadius) == false)
            {
                currentEnemy.TransitionState(stateType.secondAttack);
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
    public class FireBearSecondAttackState : EnemyState
    {
        FireBear fireBear;
        public override void OnEnter(Enemy enemy)
        {
            currentEnemy = enemy;
            fireBear = currentEnemy as FireBear;
        }
        public override void LogicUpdate()
        {
            if (fireBear.inDistance(fireBear.player.transform.position, fireBear.alertRadius) &&
               (fireBear.inDistance(fireBear.player.transform.position, fireBear.closeRadius) == false))
            {
                fireBear.secondAttack();
            }
            else if (fireBear.inDistance(fireBear.player.transform.position, fireBear.chaseRadius) &&
                    (fireBear.inDistance(fireBear.player.transform.position, fireBear.alertRadius) == false))
            {
                currentEnemy.TransitionState(stateType.chase1);
            }
            else if (fireBear.inDistance(fireBear.player.transform.position, fireBear.closeRadius))
            {
                currentEnemy.TransitionState(stateType.chase2);
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
    public class FireBearAttackState : EnemyState
    {
        FireBear fireBear;
        public override void OnEnter(Enemy enemy)
        {
            currentEnemy = enemy;
            fireBear = currentEnemy as FireBear;
        }
        public override void LogicUpdate()
        {
            if (currentEnemy.attackArea(currentEnemy.player.transform.position))
            {
                currentEnemy.attack();
            }
            else if (fireBear.inDistance(fireBear.player.transform.position, fireBear.closeRadius) &&
                     (currentEnemy.attackArea(currentEnemy.player.transform.position) == false))
            {
                currentEnemy.TransitionState(stateType.chase2);
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
