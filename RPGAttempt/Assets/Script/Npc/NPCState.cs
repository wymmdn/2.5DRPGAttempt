using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCState : BaseState
{
    protected NPC currentNPC;

    public abstract void OnEnter(NPC npc);
}
