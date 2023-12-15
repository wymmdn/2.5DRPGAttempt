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
        EventHandler.ShowDialogueEvent += ShowDialogue;//�¼�����
    }

    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= ShowDialogue;//ȡ������
    }

    private void ShowDialogue(string dialogue)//�¼�������
    {
        Debug.Log(dialogue);
        if (dialogue != string.Empty)
            panel.transform.localScale = Vector3.one;
        else
            panel.transform.localScale = Vector3.zero;
        dialogueText.text = dialogue;
    }
}
