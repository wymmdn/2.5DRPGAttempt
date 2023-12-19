using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private Transform panel;
    private Text dialogueText;
    [SerializeField]private GameObject dialogueOption;
    private Transform optionGroup;
    private Conversation conversation;
    private ConvStep currentStep;
    private List<ConvStep> options;

    private bool isShowing;
    private bool quickShow;
    public bool isOpen;

    public const int startIndex = 0;
    public const float showSpeed = 0.02f;
    public const string cnv = "cnv";    // type of ConvStep
    public const string opt = "opt";    // type of ConvStep
    public const string optionGroupName = "options";    //
    //private string currentWords;
    private void Awake()
    {
        panel = this.transform;
        dialogueText = this.GetComponentInChildren<Text>();
        optionGroup = this.transform.Find(optionGroupName);
        panel.localScale = Vector3.zero;
        isOpen = false;
        isShowing = false;
        quickShow = false;  
    }

    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += StartDialogue;//事件订阅
    }

    private void StartDialogue(Conversation conv)
    {
        this.conversation = conv;
        currentStep = conversation.convSteps.Find(i => i.index == startIndex);
        StartCoroutine(ShowDialogue(currentStep.words));
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Space))
        {
            nextWords();
        }
        if (isOpen && currentStep.type.Equals(opt, System.StringComparison.OrdinalIgnoreCase))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //currentStep = options[options.IndexOf(currentStep) + 1];
            }
        }
    }

    public void nextWords()
    {
        if (isShowing)
        {
            quickShow = true;
            return; 
        } 
        if (currentStep == null || currentStep.nextIndex < 0)
        {
            StartCoroutine(ShowDialogue(""));
            return;
        }
        currentStep = conversation.convSteps.Find(i => i.index == currentStep.nextIndex);
        if (currentStep.type.Equals(cnv, System.StringComparison.OrdinalIgnoreCase))
        {
            StartCoroutine(ShowDialogue(currentStep.words));
        }
        else if (currentStep.type.Equals(opt, System.StringComparison.OrdinalIgnoreCase))
        { 
            options = conversation.convSteps.FindAll(i => i.index == currentStep.index);
            dialogueText.text = string.Empty;
            optionGroup.localScale = Vector3.one;
            foreach (var opt in options)
            { 
                var text = Instantiate(dialogueOption,optionGroup).GetComponentInChildren<TMP_Text>();
                text.text = " -> " + opt.words;
                if (options.IndexOf(opt) == 0)
                { 
                    currentStep = opt;
                    text.fontStyle = FontStyles.Bold;
                }
            }
        }
    }

    private IEnumerator ShowDialogue(string words)
    {
        optionGroup.localScale = Vector3.zero;
        if (words == string.Empty)
        {
            panel.localScale = Vector3.zero;
            isOpen = false;
            EventHandler.CallCloseDialogueEvent();
            yield break;
        }
        panel.localScale = Vector3.one;
        yield return null;
        isOpen = true; 
        isShowing = true;
        dialogueText.text = string.Empty;
        foreach (char letter in words)
        { 
            dialogueText.text += letter;
            if (quickShow)
            {
                dialogueText.text = words;
                quickShow = false;
                break;
            }
            yield return new WaitForSeconds(showSpeed);
        }
        isShowing = false;
    }
    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= StartDialogue;//取消订阅
    }
}
