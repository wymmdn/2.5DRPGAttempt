using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour
{
    private Transform panel;
    private Text dialogueText;
    private Button nextButton;
    [SerializeField]private GameObject dialogueOption;
    private Transform optionGroup;
    private Conversation conversation;
    private ConvStep currentStep;
    private List<ConvStep> optionSteps = new List<ConvStep>();
    private List<Button> optionBtns = new List<Button>();

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
        nextButton = this.GetComponentInChildren<Button>();
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
    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= StartDialogue;//取消订阅
    }
    private void StartDialogue(Conversation conv)
    {
        this.conversation = conv;
        currentStep = conversation.convSteps.Find(i => i.index == startIndex);
        StartCoroutine(ShowDialogue(currentStep.words));
    }

    private void Update()
    {
        if (isOpen && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            nextWords();
        }
        if (isOpen && currentStep.type.Equals(opt, System.StringComparison.OrdinalIgnoreCase))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                lastOption();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextOption();
            }
        }
    }
    public void lastOption()
    {
        var idx = optionSteps.IndexOf(currentStep);
        idx--;
        if (idx < 0)
        { idx = optionSteps.Count - 1; }
        onOptionSelect(optionSteps[idx]);
    }
    public void nextOption()
    {
        var idx = optionSteps.IndexOf(currentStep);
        idx++;
        if (idx >= optionSteps.Count)
        { idx = 0; }
        onOptionSelect(optionSteps[idx]);
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
            createOptions(conversation.convSteps.FindAll(i => i.index == currentStep.index));
        }
    }
    private void createOptions(List<ConvStep> options)
    { 
        dialogueText.text = string.Empty;
        optionGroup.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        optionSteps = options;
        optionBtns = new List<Button>();
        while (optionGroup.childCount > 0)
        {
            var child = optionGroup.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
        foreach (var opt in options)
        {
            //get variable
            var go = Instantiate(dialogueOption, optionGroup);
            var btn = go.GetComponent<Button>();
            var text = go.GetComponentInChildren<TMP_Text>();
            var trigger = go.GetComponent<EventTrigger>();
            // add event
            btn.onClick.AddListener(delegate { nextWords(); });
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener(delegate { onOptionSelect(opt); });
            trigger.triggers.Add(entry);
            //build reflect
            optionBtns.Add(btn);
            text.text = " -> " + opt.words;
        }
        onOptionSelect(optionSteps[0]);
    }
    private IEnumerator ShowDialogue(string words)
    {
        if (words == string.Empty)
        {
            panel.localScale = Vector3.zero;
            isOpen = false;
            EventHandler.CallCloseDialogueEvent();
            yield break;
        }
        panel.localScale = Vector3.one;
        optionGroup.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
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
    public void onOptionPointerEnter()
    {
        
    }
    public void onOptionPointerExit()
    {
        
    }
    public void onOptionSelect(ConvStep cs)
    {
        this.currentStep = cs;
        foreach (Button btn in optionBtns)
        { 
            if(optionBtns.IndexOf(btn) == optionSteps.IndexOf(cs))
                btn.transform.GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Bold;
            else
                btn.transform.GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Normal;
        }
    }
    public void onOptionDeselect()
    { 
    
    }
}
