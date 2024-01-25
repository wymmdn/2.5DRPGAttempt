using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrangeMeowStates
{
    public class OrangeMeowIdleState : NPCState     ///////IDLE
    {
        private int preHealth;
        public override void OnEnter(NPC npc)
        {
            currentNPC = npc;
            preHealth = currentNPC.getCurHealth();
        }
        public override void LogicUpdate()
        {
            if (currentNPC.getCurHealth() < preHealth)
            {
                currentNPC.TransitionState(stateType.run);
            }
        }

        public override void PhysicsUpdate()
        {
            if (Vector2.Distance(currentNPC.transform.position,currentNPC.bornPoint) > 0.3f)
            {
                currentNPC.movement(currentNPC.bornPoint);
            }
            else
            {
                currentNPC.stopMove();
            }
        }
        public override void OnExit()
        {

        }

    }
    public class OrangeMeowRunState : NPCState     
    {
        private Vector2 runAwayPoint = Vector2.zero;
        private int preHealth;
        private float timeCnt;
        public override void OnEnter(NPC npc)
        {
            currentNPC = npc;
            preHealth = currentNPC.getCurHealth();
            timeCnt = 0f;
            Debug.Log("enter");
        }
        public override void LogicUpdate()
        {
            timeCnt += Time.deltaTime;
            if (currentNPC.getCurHealth() < preHealth)
            { 
                timeCnt = 0f;
            }
            if (timeCnt > 15f)
            {
                currentNPC.TransitionState(stateType.idle);
            }
        }

        public override void PhysicsUpdate()
        {
            if (Vector2.Distance(currentNPC.transform.position, runAwayPoint) > 0.3f)
            {
                currentNPC.movement(runAwayPoint);
            }
            else
            {
                currentNPC.stopMove();
            }
        }
        public override void OnExit()
        {

        }

    }

}
