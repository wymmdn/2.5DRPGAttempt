using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoryActor
{
    public string actorName { get; set; }    
    public Conversation conversation { get; set; }
    public void openDialogue();
}
