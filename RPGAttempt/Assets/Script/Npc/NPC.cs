using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class NPC : Role,IStoryActor
{
    public Vector3 bornPoint;
    protected NPCState currentState;
    [HideInInspector] public Dictionary<stateType, NPCState> states = new Dictionary<stateType, NPCState>();
    public string actorName { get; set; }
    public Conversation conversation { get; set; }
    //[HideInInspector] public Dictionary<stateType, EnemyState> states = new Dictionary<stateType, EnemyState>();

    protected virtual void Start()
    {
        actorName = this.name;
        conversation = StoryManager.instance.GetConversation(actorName);
    }
    public override void interact(Role role)
    {
        openDialogue();
    }

    public void openDialogue()
    {
        conversation = StoryManager.instance.GetConversation(actorName);
        EventHandler.CallShowDialogueEvent(conversation);
    }
    public void TransitionState(stateType type)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter(this);
    }
}
