using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using Cinemachine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera sceneCamera;
    [SerializeField] private float triggerRadius;
    
    
    private FireBear fireBear;
    private PlayerController player;
    [HideInInspector]public const int living = 1;
    [HideInInspector]public const int dead = 2;

    [Header("Plot Point")]
    public int treeMonsterState;
    public int fireBearState;
    public int magicBushSate;
    public bool fireBearCame;
    public bool hasFireBear;
    public bool sawFireBear;
    public bool gotMission;
    public bool completeMission;
    public bool gotMission2;
    public bool completeMission2;

    public static StoryManager instance;
    public void readStoryParam()
    {
        treeMonsterState = living;
        fireBearState = living;
        magicBushSate = 0;
        fireBearCame = false;
        hasFireBear = false;
        sawFireBear = false;
        gotMission = false;
        completeMission = false;
        gotMission2 = false;
        completeMission2 = false;
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
        hasFireBear = false ;
        player = GameObject.FindGameObjectWithTag(tagtag.player).GetComponent<PlayerController>();
        readStoryParam();
    }
    private void Update()
    {
        if (hasFireBear == false && 
            treeMonsterState == dead && 
            fireBearCame == true && 
            player.isTalking == false)
        {
            fireBear = EventHandler.CallFireBearCame();
            sceneCamera.Follow = fireBear.transform;
            sceneCamera.LookAt = fireBear.transform;
            hasFireBear = true;
        }
        if (fireBear != null && 
            sawFireBear == false && 
            Vector2.Distance(player.transform.position, fireBear.transform.position) < triggerRadius)
        {
            playerCamera.enabled = false;
            sceneCamera.enabled = true;
            player.isTalking = true;
            StartCoroutine(fireBear.startPerform());
            sawFireBear = true;
        }
    }
    private void OnEnable()
    {
        EventHandler.PerformStory += performStory;
        EventHandler.TreeMonster += setTreeMonster;
        //EventHandler.gotMisson += setGotMission;
    }
    private void performStory(string plot)
    {
        switch (plot)
        {
            case storyPlot.gotMission:
                gotMission = true;
                break;
            case storyPlot.completeMission:
                completeMission = true;
                break;
            case storyPlot.gotMission2:
                gotMission2 = true;
                break;
            case storyPlot.completeMission2:
                completeMission2 = true;
                break;
            case "":
                if (treeMonsterState == dead)
                { 
                    fireBearCame = true;
                }
                if (sawFireBear == true && fireBear != null)
                {
                    playerCamera.enabled = true;
                    sceneCamera.enabled = false;
                    fireBear.endPerform();
                }
                break;
            default:
                break;
        }
    }
    public Conversation GetConversation(string actorName)
    {
        Conversation conversation = new Conversation();
        switch (actorName)
        {
            case roleName.orangeMeow:
                {
                    if (treeMonsterState == living && gotMission == false)
                        conversation = readConvFromJson(actorName, "conv1-1");
                    else if (treeMonsterState == living && gotMission == true)
                        conversation = readConvFromJson(actorName, "conv1-2");
                    else if (treeMonsterState == dead && gotMission == false)
                        conversation = readConvFromJson(actorName, "conv3-1");
                    else if (treeMonsterState == dead && gotMission == true && completeMission == false)
                        conversation = readConvFromJson(actorName, "conv2-1");
                    else if (treeMonsterState == dead && gotMission == true && completeMission == true)
                        conversation = readConvFromJson(actorName, "conv2-2");
                    else
                        conversation = readConvFromJson(actorName, "conv0-0");
                }
                break;
            case roleName.fireBear:
                {
                    conversation = readConvFromJson(actorName, "conv11-1");
                }
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
        EventHandler.PerformStory -= performStory;
        EventHandler.TreeMonster -= setTreeMonster;
        //EventHandler.gotMisson -= setGotMission;
    }

}
