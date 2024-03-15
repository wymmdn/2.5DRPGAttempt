using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour,IInteraction, IStoryActor
{
    private Transform MaskTransform;
    private PlayerController player;

    public string actorName { get; set; }
    public Conversation conversation { get; set; }

    private void Awake()
    {
        MaskTransform = transform.GetChild(0);
        actorName = roleName.cave;
    }
    public void interact(Role role)
    {
        var hasKey = false;
        foreach (Item i in role.itemBag.items.Values)
        {
            if (i.itemName.StartsWith("caveKey"))
            { 
                hasKey = true;
                break;
            }
        }
        if (hasKey == true)
        {
            player = GameManager.instance.playerController;
            StartCoroutine(openDoor());
            StartCoroutine(player.theEnd(transform.position));
        }
    }
    private IEnumerator openDoor()
    {
        while (MaskTransform.localScale.x < 0.5f)
        { 
            MaskTransform.localScale = new Vector3(MaskTransform.localScale.x + 0.01f, MaskTransform.localScale.y,MaskTransform.localScale.z);
            yield return new WaitForSeconds(0.03f);
        }
        StoryManager.instance.gameWin = true;
        openDialogue();
    }
    public void openDialogue()
    {
        conversation = StoryManager.instance.GetConversation(actorName);
        EventHandler.CallShowDialogueEvent(conversation);
    }
}
