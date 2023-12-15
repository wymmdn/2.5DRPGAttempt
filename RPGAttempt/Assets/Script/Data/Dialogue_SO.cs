using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue_SO", menuName = "Data/Dialogue_SO", order = 0)]

public class Dialogue_SO : ScriptableObject
{
    public List<DialogueContent> dialogueList;

    public DialogueContent getContent(string name)
    {
        dialogueList = new List<DialogueContent>();
        dialogueList.Add(new DialogueContent("OrangeMeow", "lalala"));
        dialogueList.Add(new DialogueContent("notMeow", "bibibi"));
        return dialogueList.Find(i => i.masterName == name);
    }
    
}

public class DialogueContent
{
    public string masterName;
    public string contentJson;
    public DialogueContent(string name, string content)
    { 
        this.masterName = name;
        this.contentJson = content;
    }
    
}
