using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public GameObject panel;
    public Text dialogueText;
    private void Awake()
    {
        panel = this.gameObject;
        dialogueText = this.GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        Debug.Log("called");
        EventHandler.ShowDialogueEvent += ShowDialogue;//事件订阅
    }

    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= ShowDialogue;//取消订阅
    }

    private void ShowDialogue(string dialogue)//事件处理器
    {
        Debug.Log(dialogue);
        if (dialogue != string.Empty)
            panel.transform.localScale = Vector3.one;
        else
            panel.transform.localScale = Vector3.zero;
        dialogueText.text = dialogue;
    }
}
