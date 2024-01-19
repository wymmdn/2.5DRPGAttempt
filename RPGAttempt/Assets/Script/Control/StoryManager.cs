using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class StoryManager : MonoBehaviour
{
    public const int living = 1;
    public const int dead = 2;
    public int treeMonsterState;
    public int magicBushSate;
    public int seeTimesOrangeMeow;
    public bool gotMission;


    public static StoryManager instance;
    private void readStoryParam()
    {
        treeMonsterState = living;
        magicBushSate = 0;
        seeTimesOrangeMeow = 0;
        gotMission = false;
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
        EventHandler.TreeMonster += setTreeMonster;
        EventHandler.gotMisson += setGotMission;
    }
    public Conversation GetConversation(string actorName)
    {
        Conversation conversation = new Conversation();
        switch (actorName)
        {
            case roleName.orangeMeow:
                {
                    if (treeMonsterState == living && gotMission == false)
                    { 
                        conversation = readConvFromJson(actorName, "conv1-1");
                    }
                    else if (treeMonsterState == living && gotMission == true)
                        conversation = readConvFromJson(actorName, "conv1-2");
                    else if (treeMonsterState == dead && gotMission == false && seeTimesOrangeMeow == 0)
                        conversation = readConvFromJson(actorName, "conv3-1");
                    else if (treeMonsterState == dead && gotMission == true)
                        conversation = readConvFromJson(actorName, "conv2-1");
                    else
                        conversation = readConvFromJson(actorName, "conv0-0");
                }
                break;
            case "":
                break;
            default:
                break;
        }
        return conversation;
    }

    public Conversation readConvFromJson(string actorName, string convIndex)
    {
        string convJson = Resources.Load<TextAsset>("Data/content").text;
        List<Conversation> conversations = JsonConvert.DeserializeObject<List<Conversation>>(convJson);
        return conversations.Find(i => (i.actorName == actorName && i.index == convIndex));
    }
    private void setTreeMonster(int obj) => this.treeMonsterState = obj;
    private void setGotMission(bool obj) => this.gotMission = obj;
    private void OnDisable()
    {
        EventHandler.TreeMonster -= setTreeMonster;
        EventHandler.gotMisson -= setGotMission;
    }

}
