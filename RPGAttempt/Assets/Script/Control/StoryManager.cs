using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventHandler.PerformStory += performStory;//�¼�����
    }

    private void OnDisable()
    {
        EventHandler.PerformStory -= performStory;//ȡ������
    }
    private void performStory(string obj)
    {
        
    }
}
