using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static event Action<string> ShowDialogueEvent;//相当于定义委托并且声明变量
    public static void CallShowDialogueEvent(string dialogue)//
    {
        ShowDialogueEvent?.Invoke(dialogue);//触发的时候把参数传进去
    }
}
