using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractPanel : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private GameObject optionButton;
    [SerializeField] private string curOption;
    private bool isOpen;
    private bool mouseOnPanel;


    private void Awake()
    {
        isOpen = false;
    }
    private void OnEnable()
    {
        EventHandler.OpenInteractPanel += createPanel;
        EventHandler.CloseInteractPanel += destoryPanel;
    }
    private void OnDisable()
    {
        EventHandler.OpenInteractPanel -= createPanel;
        EventHandler.CloseInteractPanel -= destoryPanel;
    }
    private void Update()
    {
        if (isOpen && !mouseOnPanel && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            EventHandler.CallCloseInteractPanel(string.Empty);
        }
    }
    private void createPanel(List<string> ops,Item item, Vector3 pos)
    {
        this.item = item;        
        foreach (string str in ops)
        {
            var btn = Instantiate(optionButton, transform).GetComponent<Button>();
            btn.GetComponentInChildren<TMP_Text>().text = str;
            btn.onClick.AddListener(delegate { EventHandler.CallCloseInteractPanel(str); });
        }
        this.transform.localScale = Vector3.one;
        this.transform.parent.transform.position = pos;
        isOpen = true;
    }

    private void destoryPanel(string str)
    {
        while (transform.childCount > 0)
        {
            Transform childtf = transform.GetChild(0);
            childtf.SetParent(null);
            Destroy(childtf.gameObject);
        }
        isOpen = false;
    }
    public void pointerEnter()
    { 
        mouseOnPanel = true;
    }
    public void pointerExit()
    {
        mouseOnPanel = false;
    }
}
