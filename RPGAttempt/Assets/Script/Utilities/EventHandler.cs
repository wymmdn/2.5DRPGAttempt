using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    #region UIContorl
    public static event Action<Conversation> ShowDialogueEvent;
    public static event Action CloseDialogueEvent;
    public static event Action<List<string>,Item,Vector3> OpenInteractPanel;
    public static event Action<string> CloseInteractPanel;
    #endregion

    public static event Action<string> PerformStory;
    public static event Action<int> TreeMonster;
    public static event Func<FireBear> FireBearCame;
    public static void CallShowDialogueEvent(Conversation conv)
    {
        ShowDialogueEvent?.Invoke(conv);
    }
    public static void CallCloseDialogueEvent()
    {
        CloseDialogueEvent?.Invoke();
    }
    public static void CallOpenInteractPanel(List<string> opts,Item item,Vector3 pos)
    {
        OpenInteractPanel?.Invoke(opts,item,pos);
    }
    public static void CallCloseInteractPanel(string opt)
    {
        CloseInteractPanel?.Invoke(opt);
    }
    public static void CallPerformStory(string aa)
    {
        PerformStory?.Invoke(aa);
    }
    public static void CallTreeMonster(int state)
    {
        TreeMonster?.Invoke(state);
    }
    public static FireBear CallFireBearCame()
    {
        return FireBearCame?.Invoke();
    }
}
