using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private GameObject panel;
    private Text dialogueText;
    private Conversation conversation;
    private ConvStep currentStep;
    private bool isOpen;

    public const int startIndex = 0;
    public const string cnv = "cnv";    // type of ConvStep
    public const string opt = "opt";    // type of ConvStep
    //private string currentWords;
    private void Awake()
    {
        panel = this.gameObject;
        dialogueText = this.GetComponentInChildren<Text>();
        isOpen = false;
    }

    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += StartDialogue;//事件订阅
    }

    private void StartDialogue(Conversation conv)
    {
        this.conversation = conv;
        currentStep = conversation.convSteps.Find(i => i.index == startIndex);
        ShowDialogue(currentStep.words);
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Space))
        {
            nextWords();
        }
    }

    public void nextWords()
    {
        if (currentStep == null || currentStep.nextIndex < 0)
        {
            ShowDialogue("");
            return;
        }
        currentStep = conversation.convSteps.Find(i => i.index == currentStep.nextIndex);
        if (currentStep.type.Equals(cnv, System.StringComparison.OrdinalIgnoreCase))
        {
            ShowDialogue(currentStep.words);
        }
        else if (currentStep.type.Equals(opt, System.StringComparison.OrdinalIgnoreCase))
        { 
            //create selection button
        }
    }

    private void ShowDialogue(string words)//事件处理器
    {
        Debug.Log(words);
        if (words != string.Empty)
        {
            panel.transform.localScale = Vector3.one;
            isOpen = true;
        }
        else
        { 
            panel.transform.localScale = Vector3.zero;
            isOpen = false;
        }            
        dialogueText.text = words;
    }
    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= StartDialogue;//取消订阅
    }
}
