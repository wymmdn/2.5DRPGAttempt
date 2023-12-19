using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    public static event Action<Conversation> ShowDialogueEvent;
    public static event Action CloseDialogueEvent;
    public static event Action<string> PerformStory;
    public static event Action<int> TreeMonster;
    public static void CallShowDialogueEvent(Conversation conv)
    {
        ShowDialogueEvent?.Invoke(conv);
    }
    public static void CallCloseDialogueEvent()
    {
        CloseDialogueEvent?.Invoke();
    }
    public static void CallPerformStory(string aa)
    {
        PerformStory?.Invoke(aa);
    }
    public static void CallTreeMonster(int state)
    {
        TreeMonster?.Invoke(state);
    }
}
