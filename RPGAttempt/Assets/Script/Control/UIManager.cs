using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static event Action<string> ShowDialogueEvent;//�൱�ڶ���ί�в�����������
    public static void CallShowDialogueEvent(string dialogue)//
    {
        ShowDialogueEvent?.Invoke(dialogue);//������ʱ��Ѳ�������ȥ
    }
}
