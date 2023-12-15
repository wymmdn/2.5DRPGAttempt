using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class NPC : Role, IConversable
{
    public Vector3 bornPoint;
    protected int storyStep;
    //[HideInInspector] public Dictionary<stateType, EnemyState> states = new Dictionary<stateType, EnemyState>();

    public override void interact()
    {
        openDialogue();

    }

    public void openDialogue()
    {
        DialogueContent dialogueContent = dialogueData.getContent(roleName);
        EventHandler.CallShowDialogueEvent(getWords(dialogueContent.contentJson));
    }
    private string getWords(string contentJson)
    {

        return "Wow! who are you(>_<)?";
    }
}
