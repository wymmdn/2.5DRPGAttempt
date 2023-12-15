using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    public static event Action<string> ShowDialogueEvent;
    public static event Action<string> PerformStory;
    public static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }
    public static void CallPerformStory(string dialogue)
    {
        PerformStory?.Invoke(dialogue);
    }
}
