using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class NPC : Role,IStoryActor
{
    public Vector3 bornPoint;
    public string actorName { get; set; }
    public Conversation conversation { get; set; }
    //[HideInInspector] public Dictionary<stateType, EnemyState> states = new Dictionary<stateType, EnemyState>();

    protected virtual void Start()
    {
        actorName = this.name;
        conversation = StoryManager.instance.GetConversation(actorName);
    }
    public override void interact()
    {
        Debug.Log("called");
        EventHandler.CallShowDialogueEvent(conversation);
    }

    public void openDialogue()
    {
        //EventHandler.CallShowDialogueEvent(getWords(dialogueData.contentJson));
    }
}
