using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMeow : NPC
{

    private void OnEnable()
    {
        EventHandler.TreeMonster += treeMonsterSateChanged;
    }

    private void Update()
    {
        rb.velocity = Vector2.zero;
    }
    private void treeMonsterSateChanged(int obj)
    {
        conversation = StoryManager.instance.GetConversation(actorName);
    }
}
