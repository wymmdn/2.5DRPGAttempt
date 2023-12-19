using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class StoryManager : MonoBehaviour
{
    public const int living = 1;
    public const int dead = 2;
    public int treeMonsterState;
    public int magicBushSate;
    public int seeTimesOrangeMeow;


    public static StoryManager instance;
    private void readStoryParam()
    {
        treeMonsterState = living;
        magicBushSate = 0;
        seeTimesOrangeMeow = 0;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
        readStoryParam();
    }
    private void OnEnable()
    {
        EventHandler.TreeMonster += treeMonster;
    }
    public Conversation GetConversation(string actorName)
    {
        Conversation conversation = new Conversation();
        switch (actorName)
        {
            case "OrangeMeow":
                {
                    if (treeMonsterState == living)
                        conversation = readConvFromJson(actorName, "firstSee");
                    else if (treeMonsterState == dead)
                        conversation = readConvFromJson(actorName, "killedMonster");
                }
                break;
            case "":
                break;
            default:
                break;
        }
        return conversation;
    }
    public Conversation readConvFromJson(string actorName,string convIndex)
    {
        string convJson = Resources.Load<TextAsset>("Data/content").text;
        List<Conversation> conversations = JsonConvert.DeserializeObject<List<Conversation>>(convJson);
        return conversations.Find(i => (i.actorName == actorName && i.index == convIndex));
    }
    private void treeMonster(int obj)
    {
        treeMonsterState = obj;
    }

    private void OnDisable()
    {
        
    }

}
