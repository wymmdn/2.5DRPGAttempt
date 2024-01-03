using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBag : MonoBehaviour
{
    
    public ItemSolt clickSlot;
    [SerializeField]public RaycastHit2D rayCast;
    public Transform hitTransform;
    [SerializeField]private GameObject slotPrefab;
    [SerializeField]private Dictionary<ItemSolt,Item> items = new Dictionary<ItemSolt,Item>();
    private GridLayoutGroup bagPanel;
    private PlayerController player;
    private Role holder;
    private bool onPlace;
    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(tagtag.player).GetComponent<PlayerController>();
        bagPanel = GetComponentInChildren<GridLayoutGroup>();
        clickSlot = null;
        onPlace = false;
    }
    private void OnEnable()
    {
        EventHandler.CloseInteractPanel += performAct;
    }
    private void OnDisable()
    {
        EventHandler.CloseInteractPanel -= performAct;
    }
    private void Update()
    {
        if (onPlace == true)
        {
            clickSlot.item.transform.position = Input.mousePosition;
            if (Input.GetMouseButton(0))
            {
                var col = Physics2D.OverlapPoint(Input.mousePosition);
                hitTransform = col.transform;
                /*if (rayCast.transform.tag == tagtag.ground || rayCast.transform.tag == tagtag.createdMap)
                {
                    clickSlot.item.transform.SetParent(GameObject.FindGameObjectWithTag(tagtag.player).transform.parent);
                    clickSlot.item.transform.position = rayCast.transform.position;
                }
                else { 
                    clickSlot.item.gameObject.SetActive(false);
                    clickSlot.image.enabled = true;
                }*/
                onPlace = false;
                clickSlot = null;
            }
        }
    }
    public void addItem(Item item)
    {
        ItemSolt slot = Instantiate(slotPrefab, bagPanel.transform).GetComponent<ItemSolt>();
        slot.addItem(item);
        items.Add(slot, item);
    }
    private void performAct(string act)
    {
        Debug.Log(act);
        if (clickSlot == null) return;
        switch (act)
        { 
            case "":
                break;
            case UIString.place:
                place(clickSlot);
                break;
            default:
                break;
        }
        if(act != UIString.place)
            clickSlot = null;
    }
    private void place(ItemSolt slot)
    {
        Debug.Log("called");
        slot.item.gameObject.SetActive(true);
        slot.item.transform.SetParent(player.transform.parent);
        slot.image.enabled = false;
        onPlace = true;
    }
}
