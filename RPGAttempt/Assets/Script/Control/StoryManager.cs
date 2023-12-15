using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventHandler.PerformStory += performStory;//事件订阅
    }

    private void OnDisable()
    {
        EventHandler.PerformStory -= performStory;//取消订阅
    }
    private void performStory(string obj)
    {
        
    }
}
